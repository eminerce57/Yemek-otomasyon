<script setup>
import { ref, onMounted, defineAsyncComponent } from "vue";
import { useToast } from "primevue/usetoast";
import CompanyService from "@/service/CompanyService";
const companyTable = defineAsyncComponent(() => import("./companyTable.vue"));

const companyService = new CompanyService();
const toast = useToast();
// Modals
const formData = ref({});
const company = ref([]);
const companyModal = ref(false);
onMounted(() => {
  getList();
});

// getlist

const getList = () => {
  companyService.getCompany().then((response) => {
    food.value = response;
  });
};

//toggle modals

const toggleAddmodal = () => {
  formData.value = {};
  companyModal.value = !companyModal.value;
};
const toggleEditModal = (data) => {
  formData.value = data;
  companyModal.value = !companyModal.value;
};

//actions

const addFood = () => {
  if (!formData.value.name || !formData.value.amount) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
  } else {
    foodService.addFood(formData.value).then((response) => {
      getList();
      formData.value = {};
      companyModal.value = !companyModal.value;
    });
  }
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
                <h5>Şirket Listesi</h5>
              </template>
              <template v-slot:end>
                <Button
                  label="Şirket Ekle"
                  icon="pi pi-plus"
                  class="p-button-primary mr-2"
                  @click="toggleAddmodal"
                />
              </template>
            </Toolbar>
          </div>
          <div class="col-12">
            <div v-if="food?.length === 0" class="flex justify-content-center">
              Kayıt Bulunamadı.
            </div>
            <companyTable v-else :data="food" @toggleEditModal="toggleEditModal" @deleteFood="deleteFood" />
          </div>
        </div>
      </div>
    </div>

    <!--****************** START:: DELETE COMMUNITY MODAL ::START *******************************-->
    <Dialog
      v-model:visible="foodModal"
      :style="{ width: '760px' }"
      :header="formData.id ? 'Yemek Güncelle' : 'Yemek Ekle'"
      :modal="true"
    >
      <div class="grid">
        <div class="col-6">
          <label>Yemek İsmi</label>
          <InputText
            class="w-full"
            placeholder="İsim"
            v-model="formData.name"
          />
        </div>
        <div class="col-6">
          <label>Birim Fiyat</label>
          <InputNumber
            class="w-full"
            placeholder="Birim Fiyat"
            v-model="formData.amount"
          />
        </div>
      </div>
      <template #footer>
        <Button
          v-if="!formData.id"
          label="Kaydet"
          icon="pi pi-plus"
          class="p-button-success"
          @click="addFood"
        />
        <Button
          v-else
          label="Güncelle"
          icon="pi pi-plus"
          class="p-button-success"
          @click="updateFood"
        />
      </template>
    </Dialog>
    <!--****************** END:: DELETE COMMUNITY MODAL ::END *******************************-->
  </div>
</template>