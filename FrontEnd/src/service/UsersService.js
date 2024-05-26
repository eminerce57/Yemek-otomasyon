import { axiosApp } from "@/utils/axiosAPI";

export default class UserService {
   getUsers() {
    return axiosApp.get("User/user").then((response) => {
      return response.data;
    });
  } 
   
  saveUser(data) {
    return axiosApp.post("User/create", data).then((response) => {
      return response.data;
    });
  }
  updateUser(data) {
    return axiosApp.put("User/update", data).then((response) => {
      return response.data;
    });
  }
  deleteUser(id) {
    return axiosApp.delete(`User/delete/${id}`).then((response) => {
      return response?.data;
    });
  }
 
}
