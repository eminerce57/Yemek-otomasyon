<script setup>
import dayjs from "dayjs";

const { data } = defineProps({
  data: Array,
});

const emit = defineEmits([
  "toggleUserModal",
  "toggleDeleteUserModal",
]);

const toggleUserModal = (data) => {
  emit("toggleUserModal", data);
};

const toggleDeleteUserModal = (data) => {
  emit("toggleDeleteUserModal", data);
};
</script>

<template>
  <DataTable
    :value="data"
    stripedRows
    class="p-datatable p-2"
    responsiveLayout="scroll"
  >
    <Column header="#">
    </Column>
    <Column header="ID">
      <template #body="{ data }">
        {{ data?.id || "-" }}
      </template>
    </Column>
    <Column header="Ad">
      <template #body="{ data }">
        {{ data?.name || "-" }}
      </template>
    </Column>
    <Column header="Soyad">
      <template #body="{ data }">
        {{ data?.surname || "-" }}
      </template>
    </Column>
    <Column header="Kullanıcı Adı">
      <template #body="{ data }">
        {{ data?.username || "-" }}
      </template>
    </Column>
    <Column header="Token">
      <template #body="{ data }">
        {{ data?.token || "-" }}
      </template>
    </Column>
    <Column header="Token Time">
      <template #body="{ data }">
        {{ data?.token_time ? dayjs(data?.token_date).format("DD/MM/YYYY") : "-" }}
      </template>
    </Column>
    <Column header="Yönetici mi">
      <template #body="{ data }">
        {{ data?.is_admin ? "Evet" : "Hayır" }}
      </template>
    </Column>
    <Column header="Aktif mi">
      <template #body="{ data }">
        {{ data?.is_active ? "Evet" : "Hayır" }}
      </template>
    </Column>
    <Column header="İşlemler" headerStyle="min-width:10rem;">
      <template #body="{ data }">
        <Button
          icon="pi pi-pencil"
          class="p-button-rounded p-button-primary mr-2"
          @click="toggleUserModal({ ...data })"
          v-tooltip.top="'Düzenle'"
        />
        <Button
          v-if="data?.id !== 1"
          icon="pi pi-trash"
          class="p-button-rounded p-button-warning mt-2"
          @click="toggleDeleteUserModal(data)"
          v-tooltip.top="'Sil'"
        /> 
      </template>
    </Column>
  </DataTable>
</template>

<style lang="scss" scoped></style>
