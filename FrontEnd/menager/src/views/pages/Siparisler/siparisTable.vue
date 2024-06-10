<script setup>
import { formatDate } from "@/utils/helper";
const { data } = defineProps({
  data: Array
});


</script>
<template>
  <DataTable :value="data" stripedRows class="p-datatable p-2" responsiveLayout="scroll">
    <Column field="id" header="Siparis ID"></Column>
    <Column field="company_name" header="Sirket Adı"></Column>
    <Column field="username" header="Kullanici"></Column>  
    <Column field="name" header="Yemek"></Column>  
  </DataTable>
</template>

<style lang="scss" scoped></style>
