<script setup>
import { ref, onMounted, defineAsyncComponent } from "vue";
import { useToast } from "primevue/usetoast";
import OrderService from "@/service/OrderService";
import CommonService from "@/service/CommonService";
const orderTable = defineAsyncComponent(() => import("./orderTable.vue"));

const orderService = new OrderService();

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
};

const company = ref([]);

//toggle modals
const orderModal = ref(false);
const food = ref([]);
const toggleAddmodal = (data) => {
  console.log(data);
  formData.value.order_id = data.id
  food.value = data.food_names;
  orderModal.value = !orderModal.value;
};

const addOrder = () => {
  if (!formData.value.food_names) {
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

const Toplam = ref(0);
const ChangeFood = () => {
  Toplam.value = 0;
  formData.value.food_names.forEach(element => {
    Toplam.value += element.amount;
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
                <h5>Bugunkı Yemek Listesi</h5>
              </template>
              <template v-slot:end>
              </template>
            </Toolbar>
          </div>
          <div class="col-12">
            <orderTable :data="orders" @toggleAddmodal="toggleAddmodal" />
          </div>
        </div>
      </div>
    </div>

    <!--****************** START:: DELETE COMMUNITY MODAL ::START *******************************-->
    <Dialog v-model:visible="orderModal" :style="{ width: '760px' }"
      :header="formData.id ? 'Sipariş Güncelle' : 'Sipariş Ekle'" :modal="true">
      <div class="grid">
        <div class="col-12">
          <label>Yemek Seçiniz</label>
          <MultiSelect v-model="formData.food_names" :options="food" optionLabel="name" @change="ChangeFood"
            placeholder="Yemek Seçiniz" class="w-full" />
          </div>
          <div class="col-12">
            <strong>Toplam Tutar</strong>: {{ Toplam }} TL
          </div>
          <div class="col-12">
            <label>Kart Numarasi</label>
            <InputText class="w-full" placeholder="Kart Numarasi" />
          </div>
          <div class="col-6">
            <label >AY</label>
            <InputText class="w-full" placeholder="AY" />
          </div>
          <div class="col-6">
            <label >YIL</label>
            <InputText class="w-full" placeholder="YIL" />
          </div>
          <div class="col-6">
            <label >CVV</label>
            <InputText class="w-full" placeholder="CVV" />
          </div>
          <div class="col-6">
            <Button v-if="!formData.id" label="Siparis olustur" icon="pi pi-plus" class="p-button-success w-full mt-3"
          @click="addOrder" />
          </div>
      
      </div>
      <template #footer>
       

      </template>
    </Dialog>
    <!--****************** END:: DELETE COMMUNITY MODAL ::END *******************************-->
  </div>
</template>
