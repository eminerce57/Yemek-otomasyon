import { createStore } from "vuex";

import createPersistedState from "vuex-persistedstate";
import SecureLS from "secure-ls";
var ls = new SecureLS({ isCompression: false });
//**vuex module import */
import moduleLogin from "./Modules/loginModule";
import moduleInitAuth from "./Modules/initAuth";
import moduleSignalR from "./Modules/signalR";

export default createStore({
  modules: {
    moduleLogin,
    moduleInitAuth,
    moduleSignalR,
  },
  state: {
    isSuccess: false,
  },
  mutations: {
    SET_ISSUCCESS(state, bool) {
      state.isSuccess = bool;
    },
  },
  actions: {},
  getters: {
    returnIsSuccess: (state) => state.isSuccess,
  },
  plugins: [
    createPersistedState({
      paths: ["moduleInitAuth"],
      storage: {
        getItem: (key) => ls.get(key),
        setItem: (key, value) => ls.set(key, value),
        removeItem: (key) => ls.remove(key),
      },
    }),
  ],
});
