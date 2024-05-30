<script setup>
import { ref } from "vue";
import AppMenuItem from "./AppMenuItem.vue";
import { useStore } from "vuex";

const APP_VERSION = import.meta.env.VITE_VERSION_NUMBER;

const store = useStore();

const authMenu = (value) => {
  const menu = store.getters.getModules;
  if (menu[value] === null) return true;

  return menu[value]?.isRead;
};

const model = ref([
  {
    label: "Home",
    items: [{ label: "Ana sayfa", icon: "pi pi-fw pi-home", to: "/" }],
  },
  {
    label: "İşlemler",
    icon: "pi pi-fw pi-briefcase",
    to: "/pages",
    items: [
  
    ],
  },
  {
    label: "Tanımlar",
    icon: "pi pi-fw pi-users",
    to: "/pages",
    items: [
    {
        label: "Yemekler",
        icon: "pi pi-tags",
        to: "/pages/food",
      },
      {
        label: "Company",
        icon: "pi pi-microsoft",
        to: "/pages/company",
      },
      {
        label: "Kullanıcılar",
        icon: "pi pi-fw pi-book",
        to: "/pages/users",
      },

    ],
  },

]);
</script>

<template>
  <ul class="layout-menu">
    <template v-for="(item, i) in model" :key="item">
      <app-menu-item
        v-if="!item.separator"
        :item="item"
        :index="i"
      ></app-menu-item>
      <li v-if="item.separator" class="menu-separator"></li>
    </template>
    <li>
      <h5 class="text-center version-info product-badge status-instock">
        {{ APP_VERSION }}
      </h5>
    </li>
  </ul>
</template>

<style lang="css" scoped>
.version-info {
  position: fixed;
  left: 10rem;
  bottom: 25px;
}
</style>
