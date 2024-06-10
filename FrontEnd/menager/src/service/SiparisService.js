import { axiosApp } from "@/utils/axiosAPI";

export default class OrderService {
  GetOrder() {
    return axiosApp.get("Order/").then((response) => {
      return response.data;
    });
  }

}
