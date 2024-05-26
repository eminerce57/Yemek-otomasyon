<script setup>
import { useLayout } from "@/layout/composables/layout";
import { reactive, ref, computed } from "vue";
// import AppConfig from '@/layout/AppConfig.vue';
import { useToast } from "primevue/usetoast";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
const { layoutConfig, contextPath } = useLayout();
const router = useRouter();
const store = useStore();
//**variables and functions */

const toast = useToast();
const user = reactive({
  username: null,
  password: null,
});


const logIn = () => {
  if (!user.username || !user.password) {
    toast.add({
      severity: "warn",
      summary: "Uyarı",
      detail: "Kullanıcı adı ve/veya Şifre alanı boş olamaz",
      life: 3000,
    });
  } else {
    store.dispatch("login", { ...user }).then((response) => {
      if (store.getters.isAuthenticated) router.push("/");
    });
  }
};


</script>

<template>
  <div class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden">
    <Toast />

    <div class="flex flex-column align-items-center justify-content-center">
  <h1>YEMEK OTOMASYON</h1>   
      <div style="
          border-radius: 56px;
          padding: 0.3rem;
          background: linear-gradient(
            180deg,
            var(--primary-color) 10%,
            rgba(33, 150, 243, 0) 30%
          );
        ">
        <div class="w-full surface-card py-8 px-5 sm:px-8" style="border-radius: 53px">
          <div class="text-center mb-5">
            <div class="text-900 text-3xl font-medium mb-3"> Giriş</div>
            <span class="text-600 font-medium">Lütfen Giriş yapınız</span>
          </div>

          <div>
            <label for="email1" class="block text-900 text-xl font-medium mb-2">Kullanıcı Adı</label>
            <InputText id="email1" type="text" placeholder="" class="w-full md:w-30rem mb-5" style="padding: 1rem"
              v-model="user.email" @keyup.enter="logIn" />

            <label for="password1" class="block text-900 font-medium text-xl mb-2">Şifre</label>
            <InputText id="password1" type="password" placeholder="Şifre" class="w-full md:w-30rem mb-5"
              style="padding: 1rem" v-model="user.password" @keyup.enter="logIn" />

            <div class="flex align-items-center justify-content-between mb-5 gap-5">
              <div class="flex align-items-center">

              </div>

            </div>

            <Button label="Giriş yap" class="w-full p-3 text-xl" @click="logIn()"></Button>
          </div>
        </div>
      </div>
    </div>

    <Dialog v-model:visible="passResetModal" :style="{ width: '50vw' }" header="Şifremi Unuttum" :modal="true"
      class="p-fluid">
      <div class="field">
        <label for="name">TCKN veya Email Adresiniz</label>
        <InputText v-model.trim="resetPassTcOrEmail" required="true" />
      </div>
      <template #footer>
        <Button label="Şifre Yenile" class="p-button-text" autofocus @click="resetPassword" />
      </template>
    </Dialog>
  </div>
  <!-- <AppConfig simple /> -->
</template>

<style scoped>

</style>
