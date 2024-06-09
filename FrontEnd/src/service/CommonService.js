import { axiosApp } from "@/utils/axiosAPI";

export default class CommonService {
  getCompany() {
    return axiosApp.get("Company/").then((response) => {
      return response.data;
    });
  }

}
