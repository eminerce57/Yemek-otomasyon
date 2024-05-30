<script setup>
import { formatCurrency } from "@/utils/helper";
import { useRouter } from "vue-router";
const router = useRouter();
const { data } = defineProps({
  data: Array,
});

const emit = defineEmits(["toggleEditModal", "deleteFood"]);

const toggleEditModal = (data) => {
  emit("toggleEditModal", data);
};
const deleteCompany = (data) => {
  emit("deleteCompany", data.id);
};
const redirectToCompanyUsers = (data) => {
  router.push(`company/${data.id}`);
};
</script>

<template>
  <DataTable
    :value="data"
    stripedRows
    class="p-datatable p-2"
    responsiveLayout="scroll"
  >
    <Column field="name" header="İsim"> </Column>
    <Column header="Vergi Numarası">
      <template #body="{ data }">
        <strong>{{ data.tax_no }}</strong>
      </template>
    </Column>
    <Column header="İşlemler" headerStyle="min-width:10rem;">
      <template #body="{ data }">
        <Button
          icon="pi pi-user"
          class="p-button-rounded p-button-info mr-2"
          @click="redirectToCompanyUsers(data)"
          v-tooltip.top="'Şirket Kullanıcıları'"
        />
        <Button
          icon="pi pi-pencil"
          class="p-button-rounded p-button-primary mr-2"
          @click="toggleEditModal(data)"
          v-tooltip.top="'Düzenle'"
        />
        <Button
          icon="pi pi-trash"
          class="p-button-rounded p-button-warning mt-2"
          @click="deleteCompany(data)"
          v-tooltip.top="'Sil'"
        />
      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
