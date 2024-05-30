import { createRouter, createWebHashHistory } from "vue-router";
import AppLayout from "@/layout/AppLayout.vue";

const APP_TITLE = import.meta.env.VITE_APP_TITLE;

const router = createRouter({
  history: createWebHashHistory(),
  routes: [
    {
      path: "/",
      component: AppLayout,
      children: [
        {
          path: "/",
          name: "dashboard",
          component: () => import("@/views/Dashboard/Dashboard.vue"),
          meta: {
            title: "Dashboard",
            requiresAuth: true,
          },
        },
      ],
    },
    {
      path: "/pages/notfound",
      name: "notfound",
      component: () => import("@/views/pages/NotFound.vue"),
    },
    {
      path: "/auth/login",
      name: "login",
      component: () => import("@/views/pages/auth/Login.vue"),
      meta: {
        title: "Login",
        requiresAuth: false,
      },
    },
    {
      path: "/auth/two-factor",
      name: "twofactor",
      component: () => import("@/views/pages/auth/TwoFactor.vue"),
      meta: {
        title: "Two-Factor Authentication",
        requiresAuth: false,
      },
    },
    {
      path: "/auth/access",
      name: "accessDenied",
      component: () => import("@/views/pages/auth/Access.vue"),
    },
    {
      path: "/auth/error",
      name: "error",
      component: () => import("@/views/pages/auth/Error.vue"),
    },
    {
      path: "/:pathMatch(.*)*",
      name: "NotFound",
      component: () => import("@/views/pages/NotFound.vue"),
      meta: {
        title: "404",
        requiresAuth: false,
      },
    },



    {
      path: "/pages",
      component: AppLayout,
      children: [
        {
          path: "users",
          name: "users",
          component: () => import("@/views/pages/users/users.vue"),
          meta: {
            title: "Users",
            requiresAuth: true,
          },
        },

        {
          path: "food",
          name: "Yemekler",
          component: () => import("@/views/pages/food/food.vue"),
          meta: {
            title: "Yemekler",
            requiresAuth: true,
          },
        },
        
        
        {
          path: "company",
          name: "Şirketler",
          component: () => import("@/views/pages/company/company.vue"),
          meta: {
            title: "Şirketler",
            requiresAuth: true,
          },
        },
             
        {
          path: "company/:id/:name",
          name: "Şirket Kullanıcıları",
          component: () => import("@/views/pages/companyUsers/companyUsers.vue"),
          meta: {
            title: "Şirket Kullanıcıları",
            requiresAuth: true,
          },
        },
      ],
    },








   
  ],
});

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem("token");

  if (to.meta.requiresAuth) {
    if (token) {
      document.title = `${APP_TITLE} | ${to.meta.title}`;
      next();
    } else {
      next({ name: "login" });
      localStorage.clear();
    }
  } else {
    document.title = `${APP_TITLE} | ${to.meta.title}`;
    next();
  }
});

export default router;
