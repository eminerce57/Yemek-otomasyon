<script setup>
import { ref, onMounted, defineAsyncComponent } from "vue";
import { useToast } from "primevue/usetoast";
import OrderService from "@/service/OrderService";
import CommonService from "@/service/CommonService";
const orderTable = defineAsyncComponent(() => import("./orderTable.vue"));

const orderService = new OrderService();
const commonService = new CommonService();
const toast = useToast();
// Modals
const formData = ref({});

onMounted(() => {
  getList();
});

// getlist
const orders = ref([]);
const getList = () => {
  orderService.GetOrder().then((response) => {
  orders.value = response;
  orders.value.forEach(element => {
    if (typeof element.food_names === 'string') {
      try {
        element.food_names = JSON.parse(element.food_names);
      } catch (error) {
        console.error("JSON parse error:", error);
      }
    }
  });
});

  getCompanyList();
  getFoodList();
};

const company = ref([]);
const getCompanyList = () => {
  commonService.getCompany().then((response) => {
    company.value = response;
  });
};
const food = ref([]);
const getFoodList = () => {
  commonService.getFood().then((response) => {
    food.value = response;
  });
};
//toggle modals
const orderModal = ref(false);
const toggleAddmodal = () => {
  formData.value = {};
  orderModal.value = !orderModal.value;
};

const addOrder = () => {


  if (!formData.value.company_id || !formData.value.foods) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen zorunlu alanları doldurunuz",
      life: 3000,
    });
  } else {
    orderService.AddOrder(formData.value).then((response) => {
      orderModal.value = !orderModal.value;
      getList();
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
                <h5>Sipariş Listesi</h5>
              </template>
              <template v-slot:end>
                <Button
                  label="Sipariş Ekle"
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
            <orderTable
              v-else
              :data="orders"
              @toggleEditModal="toggleEditModal"
              @deleteFood="deleteFood"
            />
          </div>
        </div>
      </div>
    </div>

    <!--****************** START:: DELETE COMMUNITY MODAL ::START *******************************-->
    <Dialog
      v-model:visible="orderModal"
      :style="{ width: '760px' }"
      :header="formData.id ? 'Sipariş Güncelle' : 'Sipariş Ekle'"
      :modal="true"
    >
      <div class="grid">
        <div class="col-12">
          <label>Şirket Seçiniz</label>
          <Dropdown
            class="w-full"
            placeholder="Şirket"
            :options="company"
            v-model="formData.company_id"
            optionLabel="name"
            optionValue="id"
          />
        </div>
        <div class="col-12">
          <label>Yemek Seçiniz</label>
          <MultiSelect
            v-model="formData.foods"
            :options="food"
            optionLabel="name"
   
            placeholder="Yemek Seçiniz"
            class="w-full"
          />
        </div>
      </div>
      <template #footer>
        <Button
          v-if="!formData.id"
          label="Kaydet"
          icon="pi pi-plus"
          class="p-button-success"
          @click="addOrder"
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