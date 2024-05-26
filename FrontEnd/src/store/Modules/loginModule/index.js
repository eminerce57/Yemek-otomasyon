import axios from "axios";
import router from "@/router";
import Swal from "sweetalert2";
const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api/";

const moduleLogin = {
  state: () => ({
    token: "",
    name: "",
    id: "",
    isAdmin: false,
    ga: false,
  }),
  mutations: {
    setToken(state, token) {
      state.token = token;
    },
    setName(state, name) {
      state.name = name;
    },

    setId(state, id) {
      state.id = id;
    },
    setIsAdmin(state, _isAdmin) {
      state.isAdmin = _isAdmin;
    },
    setGa(state, ga) {
      state.ga = ga;
    },
    clearToken(state) {
      state.token = "";
    },
    clearGa(state) {
      state.ga = false;
    },
  },
  actions: {
    login({ commit, dispatch, state }, authData) {
      return axios
        .post(`${API_URL}User/login`, {
          username: authData.email,
          password: authData.password,
        })
        .then(
          (response) => {
            console.log(response)
            commit("setId", response.data.id);
            localStorage.setItem("id", response.data.id);
            commit("setGa", true);
            commit("setToken", response.data.token);
            commit("setName", response.data.user_name);
            commit("setIsAdmin", response.data.is_admin);
            localStorage.setItem("id", response.data.id);
            localStorage.setItem("ga", true);
            localStorage.setItem("token", response.data.token);
            localStorage.setItem("name", response.data.name);
            localStorage.setItem("surname", response.data.surname);
            localStorage.setItem("isAdmin", response.data.is_admin);
            router.push("/");
            //dispatch("setTimeoutTimer", 3600000)
          },
          (err) => {
            commit("setMessage", "Kullanıcı Adı veya Şifre Hatalı!");
            Swal.fire({
              title: `Hata!`,
              text: "Kullanıcı Adı veya Şifre Hatalı!.",
              icon: "error",
              confirmButtonText: "Tamam",
            });
            // toastr.error("Kullanıcı Adı veya Şifre Hatalı!");
          }
        );
    },
    initAuth({ commit, dispatch }) {
      let ga = localStorage.getItem("ga");
      let token = localStorage.getItem("token");
      commit("setToken", token);

      if (ga && token) {
        commit("setGa", ga);
        commit("setName", localStorage.getItem("name"));

        commit("setId", localStorage.getItem("id"));
      } else {
        if (router.path == "/auth/login") router.push("/auth/login");

        return false;
      }
    },
    logout({ commit }) {
      commit("clearToken");
      commit("clearGa");
      localStorage.removeItem("token");
      localStorage.removeItem("ga");
      router.replace("/auth/login");
    },
  },
  getters: {
    isAuthenticated(state) {
      return state.token !== "" && state.token !== null;
    },
    isGa(state) {
      return state.ga;
    },
    getNameSurname(state) {
      return state.name + " " + state.surname;
    },

    getToken(state) {
      return state.token;
    },
    getId(state) {
      return state.id;
    },
    getIsAdmin(state) {
      return state.isAdmin;
    },
  },
};

export default moduleLogin;
