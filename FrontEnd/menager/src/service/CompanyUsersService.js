import { axiosApp } from "@/utils/axiosAPI";

export default class CompanyUserService {
  getCompanyUser(id) {
    return axiosApp.get("CompanyUser/"+id).then((response) => {
      return response.data;
    });
  }
  addCompanyUser(data) {
    return axiosApp.post("CompanyUser/add", data).then((response) => {
      return response.data;
    });
  }
  updateCompanyUser(data) {
    return axiosApp.post("CompanyUser/update", data).then((response) => {
      return response.data;
    });
  }
  deleteCompanyUser(id) {
    return axiosApp.delete("CompanyUser/delete/" + id).then((response) => {
      return response.data;
    });
  }
}
