<script setup>
import { formatDate } from "@/utils/helper";
const { data } = defineProps({
  data: Array,
});

const emit = defineEmits([
  "toggleAddmodal"
]);

const toggleAddmodal = (data) => {
  emit("toggleAddmodal", data);
};
</script>

<template>
  <DataTable :value="data" stripedRows class="p-datatable p-2" responsiveLayout="scroll">
    <Column field="name" header="Şirket Adı"></Column>
    <Column header="Menu Tarihi">
      <template #body="{ data }">
        <strong>{{ formatDate(data.order_date) }}</strong>
      </template>
    </Column>
    <Column header="Yemekler">
      <template #body="{ data }">
        <Badge v-for="item in data.food_names" :key="item" :value="item.name" severity="secondary" class="mx-1" />
      </template>
    </Column>
    <Column header="Yemek Seç">
      <template #body="{ data }">
        <Button label="Sipariş Oluştur" icon="pi pi-plus" class="p-button-primary mr-2" @click="toggleAddmodal(data)" />

      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
