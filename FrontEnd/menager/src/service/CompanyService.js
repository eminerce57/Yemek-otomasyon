import { axiosApp } from "@/utils/axiosAPI";

export default class CompanyService {
  getCompany() {
    return axiosApp.get("Company/").then((response) => {
      return response.data;
    });
  }
  addCompany(data) {
    return axiosApp.post("Company/add", data).then((response) => {
      return response.data;
    });
  }
  updateCompany(data) {
    return axiosApp.post("Company/update", data).then((response) => {
      return response.data;
    });
  }
  deleteCompany(data) {
    return axiosApp.delete("Company/delete/" + data).then((response) => {
      return response.data;
    });
  }
}
