<script setup>
import { useLayout } from "@/layout/composables/layout";
import { computed } from "vue";

const { layoutConfig, contextPath } = useLayout();


</script>

<template>
  <div class="layout-footer">
    <img :src="logoUrl" alt="Logo" height="20" class="mr-2" />
    <span class="font-medium ml-2">2024s ERCE YAZILIM TÜM HAKLAR SAKLIDIR</span>
  </div>
</template>
<style lang="scss" scoped></style>
