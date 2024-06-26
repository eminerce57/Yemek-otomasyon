<script setup>
import { computed, watch, ref, onMounted } from "vue";
import AppTopbar from "./AppTopbar.vue";
import AppFooter from "./AppFooter.vue";
import AppSidebar from "./AppSidebar.vue";
// import AppConfig from './AppConfig.vue';
import { useLayout } from "@/layout/composables/layout";
import { useStore } from "vuex";

const store = useStore();

onMounted(() => {
  store.dispatch("initAuth");
});

const { layoutConfig, layoutState, isSidebarActive } = useLayout();

const outsideClickListener = ref(null);

watch(isSidebarActive, (newVal) => {
  if (newVal) {
    bindOutsideClickListener();
  } else {
    unbindOutsideClickListener();
  }
});

const containerClass = computed(() => {
  return {
    "layout-theme-light": layoutConfig.darkTheme.value === "light",
    "layout-theme-dark": layoutConfig.darkTheme.value === "dark",
    "layout-overlay": layoutConfig.menuMode.value === "overlay",
    "layout-static": layoutConfig.menuMode.value === "static",
    "layout-static-inactive":
      layoutState.staticMenuDesktopInactive.value &&
      layoutConfig.menuMode.value === "static",
    "layout-overlay-active": layoutState.overlayMenuActive.value,
    "layout-mobile-active": layoutState.staticMenuMobileActive.value,
    "p-input-filled": layoutConfig.inputStyle.value === "filled",
    "p-ripple-disabled": !layoutConfig.ripple.value,
  };
});
const bindOutsideClickListener = () => {
  if (!outsideClickListener.value) {
    outsideClickListener.value = (event) => {
      if (isOutsideClicked(event)) {
        layoutState.overlayMenuActive.value = false;
        layoutState.staticMenuMobileActive.value = false;
        layoutState.menuHoverActive.value = false;
      }
    };
    document.addEventListener("click", outsideClickListener.value);
  }
};
const unbindOutsideClickListener = () => {
  if (outsideClickListener.value) {
    document.removeEventListener("click", outsideClickListener);
    outsideClickListener.value = null;
  }
};
const isOutsideClicked = (event) => {
  const sidebarEl = document.querySelector(".layout-sidebar");
  const topbarEl = document.querySelector(".layout-menu-button");

  return !(
    sidebarEl.isSameNode(event.target) ||
    sidebarEl.contains(event.target) ||
    topbarEl.isSameNode(event.target) ||
    topbarEl.contains(event.target)
  );
};
</script>

<template>
  <div class="layout-wrapper" :class="containerClass">
    <app-topbar></app-topbar>
    <div class="layout-sidebar">
      <app-sidebar></app-sidebar>
    </div>
    <div class="layout-main-container">
      <!-- <Button icon="pi pi-arrow-left" class="p-button-raised p-button-secondary mr-2 mb-2" style="position: absolute;padding:10px;"/> -->
      <div class="layout-main">
        <router-view v-slot="{ Component }">
          <transition name="scale" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </div>
      <app-footer></app-footer>
    </div>
    <!-- <app-config></app-config> -->
    <div class="layout-mask"></div>
  </div>
</template>

<style scoped>
.scale-enter-active,
.scale-leave-active {
  transition: all 0.4s ease;
  /* -webkit-animation-duration: 0.75s;
    animation-duration: 0.75s;
    -webkit-animation-name: rotateIn;
    animation-name: rotateIn;*/
}

.scale-enter-from,
.scale-leave-to {
  opacity: 0;
  transform: scale(0.9);
  /* -webkit-animation-duration: 0.75s;
    animation-duration: 0.75s;
    -webkit-animation-name: fadeOutDown;
    animation-name: fadeOutDown; */
}
</style>
