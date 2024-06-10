import axios from "axios";
import "js-loading-overlay";
import router from "../router";

const API_URL = import.meta.env.VITE_API_URL || "http://localhost:5000/api/";

export const axiosApp = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-type": "application/json;charset=UTF-8",
    "Access-Control-Allow-Origin": "*",
    token: localStorage.getItem("token")
  },
});

axiosApp.interceptors.request.use(
  function (config) {
    config.headers.token = localStorage.getItem("token");

    JsLoadingOverlay.show({
      spinnerIcon: "ball-atom",
      spinnerColor: "#007bff",
      spinnerSize: "2x",
    });
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);
axiosApp.interceptors.response.use(
  (response) => {
    JsLoadingOverlay.hide();
    if (!response?.data?.status) {
    }
    return response;
  },
  (error) => {
    JsLoadingOverlay.hide();
     
    if (error?.response?.data?.statusCode === 401) {
      localStorage.clear();
      router.push("/auth/login");
    } else {
    }

    throw error;
  }
);

//***sitesi genel gidenler için yapıldı ilerde kaldırılabilir !!! */
export const axiosPublicApp = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-type": "application/json;",
    token: localStorage.getItem("token"),
    site: "genel",
  },
});

axiosPublicApp.interceptors.request.use(
  function (config) {
    config.headers.token = localStorage.getItem("token");
    config.headers.site = "genel";
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);
axiosPublicApp.interceptors.response.use(
  (response) => {
    if (!response.data.status) {
    }
    return response;
  },
  (error) => {
    console.log(error);
    throw error;
  }
);

export const axiosFileApp = axios.create({
  baseURL: API_URL,
  headers: {
    "Content-Type": "multipart/form-data",
    token: localStorage.getItem("token"),
    site: localStorage.getItem("site"),
  },
});

axiosFileApp.interceptors.request.use(
  function (config) {
    config.headers.token = localStorage.getItem("token");
    config.headers.site = localStorage.getItem("site");
    JsLoadingOverlay.show({
      spinnerIcon: "ball-atom",
      spinnerColor: "#007bff",
      spinnerSize: "2x",
    });
    return config;
  },
  function (error) {
    return Promise.reject(error);
  }
);
axiosFileApp.interceptors.response.use(
  (response) => {
    JsLoadingOverlay.hide();
    if (!response.data.status) {
    }
    return response;
  },
  (error) => {
    JsLoadingOverlay.hide();
    console.log(error);
  
    if (error?.response?.data?.statusCode === 401) {
      localStorage.clear();
      router.push("/auth/login");
    }

    throw error;
  }
);
