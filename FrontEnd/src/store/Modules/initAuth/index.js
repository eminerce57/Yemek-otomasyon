import axios from "axios";

const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api/";
import router from "@/router";
const moduleInitAuth = {
  state: () => ({
    modules: [],
  }),
  mutations: {
    setModules(state, modules) {
      state.modules = modules;
    },
  },
  actions: {
    initAuth({ commit, dispatch }) {

      let token = localStorage.getItem("token");
      commit("setToken", token);

      if (token) {
     
        commit("setUserName", localStorage.getItem("username"));
        commit("setId", localStorage.getItem("id"));

      } else {
        if (router.path == "/auth/login") router.push("/auth/login");

        return false;
      }
    },

    modules({ commit }, data) {
      // axios
      //   .get(`${API_URL}UserModule/ListUserModules/` + data.id, {
      //     headers: {
      //       site: "genel",
      //       token: data.token,
      //     },
      //   })
      //   .then((response) => {
      //     let modules = {};
      //     response.data.forEach((value) => {
      //       modules[value.slug] = {
      //         isRead: value.is_write,
      //         isWrite: value.is_write,
      //       };
      //     });
      //     commit("setModules", modules);
      //   })
      //   .catch((err) => {
      //     if (err.response?.status == 401) {
      //       store.commit("setMessage", err.response.data);
      //       store.dispatch("logout");
      //     }
      //   });
    },
  },
  getters: {
    getModules(state) {
      return state.modules;
    },
  },
};

export default moduleInitAuth;
