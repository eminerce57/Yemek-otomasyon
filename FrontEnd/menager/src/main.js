import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import GlobalComponents from "./plugin/globalComponent";
import store from "./store";
import "@/assets/styles.scss";

const app = createApp(App);

app.use(router);
app.use(store);
app.use(GlobalComponents);
app.mount("#app");
