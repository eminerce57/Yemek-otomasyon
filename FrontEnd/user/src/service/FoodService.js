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
  updateFood(data) {
    return axiosApp.post("Food/update",data).then((response) => {
      return response.data;
    });
  };
  deleteFood(data) {
    return axiosApp.delete("Food/delete/"+data).then((response) => {
      return response.data;
    });
  } 
}
