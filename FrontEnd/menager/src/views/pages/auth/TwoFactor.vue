<script setup>
import { useLayout } from '@/layout/composables/layout';
import { reactive, computed } from 'vue';
// import AppConfig from '@/layout/AppConfig.vue';
import { useToast } from 'primevue/usetoast';
const { layoutConfig, contextPath } = useLayout();
import { useRouter } from "vue-router";
import {useStore} from "vuex";
const router = useRouter();
const store = useStore();

const toast = useToast();
const user = reactive({
    ga:null
})
// const checked = ref(false);
const logoUrl = computed(() => {
    return `${contextPath}layout/images/${layoutConfig.darkTheme.value ? 'logo-white' : 'logo-dark'}.png`;
});

const logIn=()=>{
    if(!user.ga){
        toast.add({ severity: 'warn', summary: 'Uyarı', detail: 'GA code boş olamaz', life: 3000 });
   }else{
       store.dispatch("ga",{...user}).then((response) => {
        if (store.getters.isGa) router.push("/");
      });
   }
}

</script>

<template>
    <div class="surface-ground flex align-items-center justify-content-center min-h-screen min-w-screen overflow-hidden">
        <Toast />
        <div class="flex flex-column align-items-center justify-content-center">
            <img :src="logoUrl" alt="Sakai logo" class="mb-5 w-6rem flex-shrink-0" />
            <div style="border-radius: 56px; padding: 0.3rem; background: linear-gradient(180deg, var(--primary-color) 10%, rgba(33, 150, 243, 0) 30%)">
                <div class="w-full surface-card py-8 px-5 sm:px-8" style="border-radius: 53px">
                    <div class="text-center mb-5">
                        <div class="text-900 text-3xl font-medium mb-3">İki aşamalı doğrulama</div>
                        <span class="text-600 font-medium"></span>
                    </div>

                    <div>
                        <!-- <label for="email1" class="block text-900 text-xl font-medium mb-2">Email</label> -->
                        <InputText id="code1" type="text" placeholder="Kod" class="w-full md:w-30rem mb-5" style="padding: 1rem" v-model="user.ga" @keyup.enter="logIn"/>

                        <div class="flex align-items-center justify-content-between mb-5 gap-5">
                            
                            <!-- <div class="flex align-items-center">
                                <Checkbox v-model="checked" id="rememberme1" binary class="mr-2"></Checkbox>
                                <label for="rememberme1">Remember me</label>
                            </div> -->
                            <!-- <a class="font-medium no-underline ml-2 text-right cursor-pointer" style="color: var(--primary-color)">Forgot password?</a> -->
                        </div>

                        <Button label="Giriş yap" class="w-full p-3 text-xl" @click="logIn();"></Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- <AppConfig simple /> -->
</template>

<style scoped>
.pi-eye {
    transform: scale(1.6);
    margin-right: 1rem;
}

.pi-eye-slash {
    transform: scale(1.6);
    margin-right: 1rem;
}
</style>
