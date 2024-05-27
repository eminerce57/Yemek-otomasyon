import axios from "axios";
import router from "@/router";
import Swal from "sweetalert2";
const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api/";

const moduleLogin = {
  state: () => ({
    id:0,
    token: "",
    username: "",
    admin:false,
  }),
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    setUserName(state, username) {
      state.username = username;
    },
    setAdmin(state, admin) {
      state.admin = admin;
    },
    setId(state, admin) {
      state.admin = admin;
    },
  },
  actions: {
    login({ commit, dispatch, state }, authData) {
      return axios
        .post(`${API_URL}User/login`, {
          username: authData.username,
          password: authData.password,
        })
        .then(
          (response) => {

            commit("setToken", response.data.token);
            commit("setUserName", response.data.username);
            commit("setAdmin", response.data.admin);
            localStorage.setItem("id", response.data.id);
            localStorage.setItem("token", response.data.token);
            localStorage.setItem("username", response.data.username);
            localStorage.setItem("admin", response.data.admin);
            router.push("/");
          },
          (err) => {
         
            Swal.fire({
              title: `Hata!`,
              text: "Kullanıcı Adı veya Şifre Hatalı!.",
              icon: "error",
              confirmButtonText: "Tamam",
            });
         
          }
        );
    },
    logout({ commit }) {

      localStorage.removeItem("token");
      router.replace("/auth/login");
    },
  },
  getters: {
    isAuthenticated(state) {
      return state.token !== "" && state.token !== null;
    },
   
    getUsername(state) {
      return state.username;
    },
    getToken(state) {
      return state.token;
    },
    getId(state) {
      return state.id;
    },
    getAdmin(state) {
      return state.admin;
    },
  },
};

export default moduleLogin;
