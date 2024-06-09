<script setup>
import {formatCurrency}from "@/utils/helper";
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
    <Column field="name" header="İsim">
    </Column>
    <Column header="Birim Fiyat">
      <template #body="{ data }">
      <strong>{{ formatCurrency(data.amount) }}</strong>
      </template>
    </Column>
    <Column header="İşlemler" headerStyle="min-width:10rem;">
      <template #body="{ data }">
        <Button
          icon="pi pi-pencil"
          class="p-button-rounded p-button-primary mr-2"
          @click="toggleEditModal(data)"
          v-tooltip.top="'Düzenle'"
        />
        <Button
          icon="pi pi-trash"
          class="p-button-rounded p-button-warning mt-2"
          @click="deleteFood(data)"
          v-tooltip.top="'Sil'"
        /> 
      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
