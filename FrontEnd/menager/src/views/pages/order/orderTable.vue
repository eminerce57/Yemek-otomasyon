<script setup>
import { formatDate } from "@/utils/helper";
const { data } = defineProps({
  data: Array,
});

const calculateTotal = (foodNames) => {
  return foodNames.reduce((total, item) => total + item.amount, 0);
};
</script>

<template>
  <DataTable
    :value="data"
    stripedRows
    class="p-datatable p-2"
    responsiveLayout="scroll"
  >
    <Column field="name" header="Şirket Adı"></Column>
    <Column header="Sipariş Tarihi">
      <template #body="{ data }">
        <strong>{{ formatDate(data.order_date) }}</strong>
      </template>
    </Column>
    <Column header="Yemekler">
      <template #body="{ data }">
        <Badge v-for="item in data.food_names" :key="item" :value="item.name" severity="secondary" class="mx-1" />
      </template>
    </Column>
    <Column header="Genel Toplam">
      <template #body="{ data }">
        <strong>{{ calculateTotal(data.food_names) }} TL</strong>
      </template>
    </Column>

    <Column header="Tamamlanmış?">
      <template #body="{ data }">
        <strong>{{ data.is_okey ? 'Evet':'Hayır' }}</strong>
      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
