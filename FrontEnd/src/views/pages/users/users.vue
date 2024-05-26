<script setup>
import { ref, onMounted} from "vue";
import { useToast } from "primevue/usetoast";
import UserService from "@/service/UsersService";

import UsersTable from "./usersTable.vue";

const userService = new UserService();

const toast = useToast();
const users = ref([]);

// Modals
const userModal = ref(false);
const deleteUserModal = ref(false);

const submitted = ref(false);

const formData = ref({});

onMounted(() => {
  getList();
});

const getList = (args) => {

  userService.getUsers().then((response) => {
    if (response) {
      users.value = response;
    }
  });
};

// Fill forms
const fillUserForm = (data) => {
  const { birth_date, ...rest } = data;

  formData.value = {
    birth_date: birth_date?.length > 0 ? birth_date.split("T")[0] : "",
    ...rest,
  };
};

// Toggle modals
const toggleUserModal = (data) => {
  fillUserForm(data);

  submitted.value = false;
  userModal.value = !userModal.value;
};

const toggleDeleteUserModal = (data) => {
  fillUserForm(data);

  submitted.value = false;
  deleteUserModal.value = !deleteUserModal.value;
};


const saveUser = () => {
  submitted.value = true;

if(!formData.value.username && !formData.value.name  && !formData.value.surname && !formData.value.password && !formData.value.is_admin)
{
  toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
}
  else {
    const _data = { ...formData.value };

    userService.saveUser(_data).then((response) => {
      console.log(formData.value)
        toast.add({
          severity: "success",
          summary: "Başarılı!",
          detail: "İşlem Başarılı şekilde Tamamlanmıştır.",
          life: 3000,
        });
        userModal.value = false;
        getList();
      
    });
  }
};
const updateUser = () => {
  submitted.value = true;
  const isValidated = formData?.value?.id;

  if (!isValidated) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
  } else {
    const _data = { ...formData.value };
    userService.updateUser(_data).then((response) => {
        toast.add({
          severity: "success",
          summary: "Başarılı!",
          detail: "İşlem Başarılı şekilde Tamamlanmıştır.",
          life: 3000,
        });
        userModal.value = false;
        getList();
      
    });
  }
};
const deleteUser = () => {
  const _id = formData?.value?.id;
  userService.deleteUser(_id).then((response) => {
      toast.add({
        severity: "success",
        summary: "Başarılı!",
        detail: "İşlem Başarılı şekilde Tamamlanmıştır.",
        life: 3000,
      });
      deleteUserModal.value = false;
      getList();
    
  });
};

</script>

<template>
  <div className="grid">
    <Toast />
    <div class="col-12">
      <div class="card">
        <div class="grid">
          <div class="col-12">
            <Toolbar class="mb-4">
              <template v-slot:start>
                <h5>Kullanıcı Listesi</h5>
              </template>
              <template v-slot:end>
                <Button label="Yeni Kullanıcı" icon="pi pi-plus" class="p-button-primary mr-2" @click="toggleUserModal" />
              </template>
            </Toolbar>
          </div>
          <div class="col-12">
            <div v-if="users?.length === 0" class="flex justify-content-center">
              Kayıt Bulunamadı.
            </div>
            <UsersTable v-else :data="users" @toggleUserModal="toggleUserModal"
              @toggleDeleteUserModal="toggleDeleteUserModal" />
          </div>
        </div>
      </div>
    </div>

    <!--****************** START:: DELETE COMMUNITY MODAL ::START *******************************-->
    <Dialog v-model:visible="deleteUserModal" :style="{ width: '450px' }" header="Kullanıcıyı Sil" :modal="true">
      <div class="flex align-items-center justify-content-center">
        <i class="pi pi-exclamation-triangle mr-3 text-4xl" />

        <h4>Kayıt Silinecektir. Bu işleme devam etmek istiyor musunuz?</h4>
      </div>
      <template #footer>
        <Button label="Sil" icon="pi pi-trash" class="p-button-danger" @click="deleteUser" />
      </template>
    </Dialog>
    <!--****************** END:: DELETE COMMUNITY MODAL ::END *******************************-->

    <!-- *****************SAVE /UPDATE MODEL START**************************** -->
    <Dialog v-model:visible="userModal" :style="{ width: '50vw' }" :header="formData?.id !== 0 && !formData?.id
        ? 'Yeni Kullanıcı'
        : 'Kullanıcı Güncelle'
      " :modal="true" class="p-fluid">
      <div class="formgrid grid">
        <div class="field col-6 ">
          <label for="first-name">Ad</label>
          <input v-model.trim="formData.name" id="first-name" type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            :class="{
              'c-invalid': submitted && !formData.name,
            }" />
        </div>
        <div class="field col-6 ">
          <label for="last-name">Soyad</label>
          <input v-model.trim="formData.surname" id="last-name" type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            :class="{
              'c-invalid': submitted && !formData.surname,
            }" />
        </div>
        <div class="field" :class="!formData.id ? 'col-6':'col-10'" >
          <label for="username">Kullanıcı Adı</label>
          <input v-model.trim="formData.username" id="username" type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            :class="{
              'c-invalid': submitted && !formData.username,
            }" />
        </div>
        <div class="field col-4" v-if="!formData.id">
      <label for="password">Şifre</label>
      <input v-model.trim="formData.password" id="password" type="password"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
            :class="{
              'c-invalid': submitted && !formData.password,
            }" />
      </div>
        <div class="field col-2">
          <div class="flex align-items-center gap-4">
            <div class="flex flex-column justify-content-center gap-2">
              <label for="is-admin">Admin mi?</label>
              <InputSwitch v-model="formData.is_admin" />
            </div>
          </div>
        </div>
      </div>

      <template #footer>
        <Button label="İptal" icon="pi pi-times" class="p-button-text" @click="userModal = false" />
        <Button v-if="formData?.id !== 0 && !formData?.id" label="Kaydet" icon="pi pi-check" class="p-button-text"
          autofocus @click="saveUser" />
        <Button v-if="formData?.id || formData?.id === 0" label="Güncelle" icon="pi pi-check" class="p-button-text"
          autofocus @click="updateUser" />
      </template>
    </Dialog>
    <!-- *****************SAVE /UPDATE MODEL END**************************** -->
  </div>
</template>

<style scoped>
h6,
p {
  margin: 0 !important;
  padding: 4px !important;
}
</style>
