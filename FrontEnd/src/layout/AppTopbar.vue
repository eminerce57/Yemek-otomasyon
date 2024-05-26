<script setup>
import { ref, computed, onMounted, onBeforeUnmount } from "vue";
import { useLayout } from "@/layout/composables/layout";
import { useStore } from "vuex";

import { useToast } from "primevue/usetoast";
import { useRouter } from "vue-router";

const toast = useToast();
const router = useRouter();
const { changeThemeSettings, layoutConfig, onMenuToggle, contextPath } =
  useLayout();

const outsideClickListener = ref(null);
const topbarMenuActive = ref(false);
const store = useStore();

const Dclass = ref(JSON.parse(localStorage.getItem("dark-mode")));
const _passData = ref(null);
const _changePassDialog = ref(false);

onBeforeUnmount(() => {
  unbindOutsideClickListener();
});

const topbarMenuClasses = computed(() => {
  return {
    "layout-topbar-menu-mobile-active": topbarMenuActive.value,
  };
});

const unbindOutsideClickListener = () => {
  if (outsideClickListener.value) {
    document.removeEventListener("click", outsideClickListener);
    outsideClickListener.value = null;
  }
};

onMounted(() => {
  const isDarkMode = JSON.parse(localStorage.getItem("dark-mode")) || false;

  const Switch = ref(document.getElementById("switchD"));
  Switch._value.checked = isDarkMode;

  darkmode(isDarkMode);
});

const onChangeTheme = (theme, mode) => {
  const elementId = "theme-css";
  const linkElement = document.getElementById(elementId);
  const cloneLinkElement = linkElement.cloneNode(true);
  const newThemeUrl = linkElement
    .getAttribute("href")
    .replace(layoutConfig.theme.value, theme);
  cloneLinkElement.setAttribute("id", elementId + "-clone");
  cloneLinkElement.setAttribute("href", newThemeUrl);
  cloneLinkElement.addEventListener("load", () => {
    linkElement.remove();
    cloneLinkElement.setAttribute("id", elementId);
    changeThemeSettings(theme, mode === "dark");
  });
  linkElement.parentNode.insertBefore(
    cloneLinkElement,
    linkElement.nextSibling
  );
};

const inputListener = (e) => {
  let theme = e.target.checked;

  darkmode(theme);
};

const darkmode = (theme) => {
  localStorage.setItem("dark-mode", theme);
  if (theme === true) {
    Dclass.value = true;
    onChangeTheme("bootstrap4-dark-blue", "dark");
  } else if (theme === false) {
    Dclass.value = false;
    onChangeTheme("bootstrap4-light-blue", "light");
  }
};

const items = ref([
  {
    label: "Profilim",
    icon: "pi pi-user",
    command: () => {
      router.push("/Profile");
    },
  },
  {
    label: "Şifre Değiştir",
    icon: "pi pi-key",
    command: () => {
      _passData.value = {};
      _changePassDialog.value = true;
    },
  },
  {
    label: "Çıkış Yap",
    icon: "pi pi-sign-out",
    command: () => {
      localStorage.setItem("token", null);
      store.dispatch("logout");
    },
  },
]);
const logoUrl = computed(() => {
  return `${contextPath}layout/images/${
    layoutConfig.darkTheme.value ? "logo-white" : "logo-dark"
  }.png`;
});

const changePassword = () => {
  if (
    !_passData.value.oldpassword ||
    !_passData.value.password ||
    !_passData.value.repassword
  ) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen tüm alanları doldurunuz.",
      life: 3000,
    });
  } else if (_passData.value.password.length < 5) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Lütfen daha güçlü bir şifre belirleyiniz.",
      life: 3000,
    });
  } else if (_passData.value.password != _passData.value.repassword) {
    toast.add({
      severity: "warn",
      summary: "Uyarı!",
      detail: "Şifreler Uyuşmuyor",
      life: 3000,
    });
  } else {
    usersService
      .changePasswordForLoginUser({
        id: store.getters.getId,
        NewPassword: _passData.value.password,
      })
      .then((res) => {
        if (res) {
          toast.add({
            severity: "success",
            summary: "Başarılı!",
            detail: res.data,
            life: 3000,
          });
          _passData.value = {};
          _changePassDialog.value = false;
        }
      });
  }
};
</script>

<template>
  <div class="layout-topbar">
    <Toast />
    <div style="width: 15%;">
      <router-link to="/" class="layout-topbar-logo" style="width: 100%;">
        <img  :src="logoUrl" alt="logo" />
PANEL
      </router-link>
    </div>
    <i
      :class="Dclass ? 'pi pi-moon' : 'pi pi-sun'"
      style="font-size: 1.2rem"
    ></i>
    <input v-on:change="inputListener" type="checkbox" id="switchD" /><label
      :style="Dclass ? 'background:#fff' : ''"
      class="switch-D"
      for="switchD"
      >Toggle</label
    >

    <button
      class="p-link layout-menu-button layout-topbar-button"
      @click="onMenuToggle()"
    >
      <i class="pi pi-bars"></i>
    </button>

    <button
      class="p-link layout-topbar-menu-button layout-topbar-button"
      @click="onTopBarMenuButton()"
    >
      <i class="pi pi-ellipsis-v"></i>
    </button>
    <div class="layout-topbar-menu" :class="topbarMenuClasses">
      <Button
        class="p-button-text p-button-plain p-button-rounded"
        @click="$refs.menu1.toggle($event)"
        ><i class="pi pi-cog" style="font-size: 2rem"></i
      ></Button>
      <Menu ref="menu1" :popup="true" :model="items"></Menu>
    </div>
  </div>

  <Dialog
    v-model:visible="_changePassDialog"
    :style="{ width: '450px' }"
    header="Şifre Değiştir"
    :modal="true"
    class="p-fluid"
  >
    <div class="field col">
      <label for="name">Eski Şifreniz </label>
      <InputText v-model.trim="_passData.oldpassword" />
    </div>
    <div class="field col">
      <label for="name">Yeni Şifre </label>
      <Password v-model="_passData.password" />
      <!-- <InputText  v-model.trim="_passData.password"   />                                   -->
    </div>
    <div class="field col">
      <label for="name">Yeni Şifre Tekrar</label>
      <Password v-model="_passData.repassword" />
      <!-- <InputText  v-model.trim="_passData.repassword"   />                                   -->
    </div>
    <template #footer>
      <Button
        label="İptal"
        icon="pi pi-times"
        class="p-button-text"
        @click="_changePassDialog = false"
      />
      <Button
        label="Güncelle"
        icon="pi pi-check"
        class="p-button-text"
        autofocus
        @click="changePassword"
      />
    </template>
  </Dialog>
</template>

<style lang="scss" scoped>
@media screen and (max-width: 1336px) {
  .top-badges {
    display: none !important;
  }
}

.top-badges {
  display: flex;
  align-items: center;
  justify-content: space-around;
}

input[type="checkbox"] {
  height: 0;
  width: 0;
  visibility: hidden;
}

.switch-D {
  cursor: pointer;
  text-indent: -9999px;
  width: 45px;
  height: 25px;
  background: #2a323d;
  display: block;
  border-radius: 100px;
  position: relative;
}

.switch-D:after {
  content: "";
  position: absolute;
  top: 5px;
  left: 5px;
  width: 15px;
  height: 15px;
  background: #fff;
  border-radius: 90px;
  -webkit-transition: 0.7s;
  transition: 0.7s;
}

input:checked + .switch-Dl {
  background: #fff;
}

input:checked + .switch-D:after {
  content: "";
  width: 15px;
  height: 15px;
  background: #2a323d;
  left: calc(100% - 5px);
  -webkit-transform: translateX(-100%);
  transform: translateX(-100%);
}

.p-speeddial-button.p-button.p-button-icon-only {
  width: 2rem !important;
  height: 2rem !important;
}
</style>
