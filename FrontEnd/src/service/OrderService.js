import { axiosApp } from "@/utils/axiosAPI";

export default class OrderService {

    AddOrder(data) {
    return axiosApp.post("Order/add",data).then((response) => {
      return response.data;
    });
  } 
 
}
