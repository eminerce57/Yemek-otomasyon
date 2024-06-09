<script setup>
import {formatDate}from "@/utils/helper";
const { data } = defineProps({
  data: Array,
});

const emit = defineEmits([
  "toggleEditModal",
  "deleteFood",
]);

const toggleEditModal = (data) => {
  emit("toggleEditModal", data);
};
const deleteFood = (data) => {
  emit("deleteFood", data.id);
};

</script>

<template>
  <DataTable
    :value="data"
    stripedRows
    class="p-datatable p-2"
    responsiveLayout="scroll"
  >
    <Column field="name" header="Şirket Adı">
    </Column>
    <Column header="Sipariş Tarihi">
      <template #body="{ data }">
      <strong>{{ formatDate(data.order_date) }}</strong>
      </template>
    </Column>
    <Column header="Sipariş Tarihi">
      <template #body="{ data }">
      <strong v-for="item in data.food_names" :key="item">  {{ item }}</strong>,
      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
