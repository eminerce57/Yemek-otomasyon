import dayjs from "dayjs";
import "dayjs/locale/tr";
dayjs.locale("tr");
const VITE_IMAGE_ROOT_PATH = import.meta.env.VITE_IMAGE_ROOT_PATH;

export function formatDate(dateString) {
  if (!dateString) return "";
  const date = dayjs(dateString);
  return date.format("DD/MM/YYYY HH:mm");
}
export function formatDateInput(dateString) {
  const date = dayjs(dateString);
  return date.format("YYYY-MM-DD");
}
export function formatMinutes(_minutes) {
  var hours = Math.floor(_minutes / 60);
  var minutes = _minutes % 60;

  return `${hours} saat ${minutes} dk`;
}
export function formatPhone(value) {
  return value
    .replace(/[^0-9]/g, "")
    .replace(/(\d{3})(\d{3})(\d{2})(\d{2})/, "$1-$2-$3-$4");
}
//ilk değişken string,ikinci değişken kısaltılacak karakter sınırı değer verilmezse default olarak 20 karakter.
export function shortName(desc, size = 20) {
  return desc?.length > size ? desc?.substr(0, size) + " ..." : desc;
}

export function formatCurrency(value) {
  if (!value) return "0";
  return value.toLocaleString("tr-TR", { style: "currency", currency: "TRY" });
}




export const IMG_BASE_URL = VITE_IMAGE_ROOT_PATH;