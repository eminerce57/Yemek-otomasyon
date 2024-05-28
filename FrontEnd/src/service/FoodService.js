import { axiosApp } from "@/utils/axiosAPI";

export default class FoodService {

   getFood() {
    return axiosApp.get("Food/").then((response) => {
      return response.data;
    });
  } 
  addFood(data) {
    return axiosApp.post("Food/add",data).then((response) => {
      return response.data;
    });
  } 
 
}
