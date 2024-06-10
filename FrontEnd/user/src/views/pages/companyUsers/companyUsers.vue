<script setup>
import { ref, onMounted, defineAsyncComponent } from "vue";
import { useToast } from "primevue/usetoast";
import CompanyUserService from "@/service/CompanyUsersService";
import CommonService from "@/service/CommonService";
import { useRouter, useRoute } from "vue-router";
const companyUsersTable = defineAsyncComponent(() =>
  import("./companyUsersTable.vue")
);
const companyUserService = new CompanyUserService();
const commonService = new CommonService();
const route = useRoute();
const toast = useToast();
const users = ref([]);

// Modals
const userModal = ref(false);
const formData = ref({});
const companyName = ref("");
const companyId = ref(0);
onMounted(() => {
  companyName.value = route.params.name;
  companyId.value = route.params.id;
  getList();
});

const getList = () => {
  companyUserService.getCompanyUser(companyId.value).then((response) => {
    if (response) {
      users.value = response;
    }
  });
};

// Toggle modals
const toggleUserModal = (data) => {
  formData.value = data;
  userModal.value = !userModal.value;
};

const saveUser = () => {
  formData.value.company_id = companyId.value;
  if (
    !formData.value.username &&
    !formData.value.name &&
    !formData.value.surname &&
    !formData.value.password
  ) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
  } else {
    companyUserService.addCompanyUser(formData.value).then((response) => {
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
  if (
    !formData.value.username &&
    !formData.value.name &&
    !formData.value.surname &&
    !formData.value.password
  ) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
  } else {
    companyUserService.updateCompanyUser(formData.value).then((response) => {
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
const deleteUser = (data) => {
  companyUserService.deleteCompanyUser(data.id).then((response) => {
 
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
                <h5>{{ companyName }} kullanıcı Listesi</h5>
              </template>
              <template v-slot:end>
                <Button
                  label="Yeni Kullanıcı"
                  icon="pi pi-plus"
                  class="p-button-primary mr-2"
                  @click="toggleUserModal"
                />
              </template>
            </Toolbar>
          </div>
          <div class="col-12">
            <div v-if="users?.length === 0" class="flex justify-content-center">
              Kayıt Bulunamadı.
            </div>
            <companyUsersTable
              v-else
              :data="users"
              @toggleUserModal="toggleUserModal"
              @toggleDeleteUserModal="deleteUser"
            />
          </div>
        </div>
      </div>
    </div>

    <!-- *****************SAVE /UPDATE MODEL START**************************** -->
    <Dialog
      v-model:visible="userModal"
      :style="{ width: '50vw' }"
      :header="
        formData?.id !== 0 && !formData?.id
          ? 'Yeni Kullanıcı'
          : 'Kullanıcı Güncelle'
      "
      :modal="true"
      class="p-fluid"
    >
      <div class="formgrid grid">
        <div class="field col-6">
          <label for="first-name">Ad</label>
          <input
            v-model.trim="formData.name"
            id="first-name"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field col-6">
          <label for="last-name">Soyad</label>
          <input
            v-model.trim="formData.surname"
            id="last-name"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>

        <div class="field" :class="!formData.id ? 'col-6' : 'col-10'">
          <label for="username">Kullanıcı Adı</label>
          <input
            v-model.trim="formData.username"
            id="username"
            type="text"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
        </div>
        <div class="field col-4" v-if="!formData.id">
          <label for="password">Şifre</label>
          <input
            v-model.trim="formData.password"
            id="password"
            type="password"
            class="text-base text-color surface-overlay p-2 border-1 border-solid surface-border border-round appearance-none outline-none focus:border-primary w-full"
          />
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
        <Button
          v-if="formData?.id !== 0 && !formData?.id"
          label="Kaydet"
          icon="pi pi-check"
          class="p-button-text"
          autofocus
          @click="saveUser"
        />
        <Button
          v-if="formData?.id || formData?.id === 0"
          label="Güncelle"
          icon="pi pi-check"
          class="p-button-text"
          autofocus
          @click="updateUser"
        />
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
