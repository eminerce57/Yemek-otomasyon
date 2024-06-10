<script setup>
import { ref, onMounted, defineAsyncComponent } from "vue";
import { useToast } from "primevue/usetoast";
import OrderService from "@/service/OrderService";
import CommonService from "@/service/CommonService";
const orderTable = defineAsyncComponent(() => import("./SiparisTable.vue"));

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
  orderService.GetSiparisOrder().then((response) => {
    orders.value = response;
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
                <h5>Gelen Sipariş Listesi</h5>
              </template>
           
            </Toolbar>
          </div>
          <div class="col-12">
            <orderTable :data="orders" />
          </div>
        </div>
      </div>
    </div>


  </div>
</template>