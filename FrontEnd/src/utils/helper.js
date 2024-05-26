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

export function emailValid(email) {
  const emailRegex = /^([A-Za-z0-9_\-.])+@([A-Za-z0-9_\-.])+\.([A-Za-z]{2,4})$/;
  return emailRegex.test(email);
}

export function passValid(pass) {
  const passRegex =
    /^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{6,}$/;
  return passRegex.test(pass);
}
export function urlify(text) {
  var urlRegex = /(https?:\/\/[^\s]+)/g;
  return text.replace(urlRegex, function (url) {
    return '<a href="' + url + '">' + url + "</a>";
  });
}

export const getUpdatedList = ({ list, date, total, app }) => {
  if (list.length > 0) {
    const updatedList = list.map((item, idx) =>
      item.id === Number(app)
        ? {
            pingDate: date,
            total,
            id: item.id,
            name: item.name,
            isChange: date !== item.date,
            isComplate: item.isComplate,
          }
        : item
    );
    return updatedList;
  }

  return [];
};

export const imageSekeleton =
  "https://static.vecteezy.com/system/resources/previews/004/141/669/non_2x/no-photo-or-blank-image-icon-loading-images-or-missing-image-mark-image-not-available-or-image-coming-soon-sign-simple-nature-silhouette-in-frame-isolated-illustration-vector.jpg";
export const getDaysString = (days) => {
  let str = "";
  if (days.length < 1) return str;

  days.forEach(({ value }, idx) => {
    str += `${value},`;
  });

  return str.slice(0, str.length - 1);
};

export const genders = [
  { label: "Erkek", value: "1" },
  { label: "Kadın", value: "2" },
  { label: "Tümü", value: "3" },
];

export const getGenderById = (id) => {
  if (!id) return null;

  return genders.find((item) => Number(item.value) === id);
};

export const weekDays = [
  { label: "Monday", value: "monday" },
  { label: "Tuesday", value: "tuesday" },
  { label: "Wednesday", value: "wednesday" },
  { label: "Thursday", value: "thursday" },
  { label: "Friday", value: "friday" },
  { label: "Saturday", value: "saturday" },
  { label: "Sunday", value: "sunday" },
];
export const convertDaysToTurkish = (days) => {
  const daysInEnglish = [
    "monday",
    "tuesday",
    "wednesday",
    "thursday",
    "friday",
    "saturday",
    "sunday",
  ];
  const daysInTurkish = [
    "pazartesi",
    "salı",
    "çarşamba",
    "perşembe",
    "cuma",
    "cumartesi",
    "pazar",
  ];
  if (days.split(",").length >= 7) {
    return "Tüm günler";
  }
  return days
    .split(",")
    .map((day) => {
      const index = daysInEnglish.indexOf(day.trim().toLowerCase());
      if (index !== -1) {
        return daysInTurkish[index];
      } else {
        return "Bilinmeyen gün";
      }
    })
    .join(", ");
};
export const getCourseDetailById = (params) => {
  const { id, data } = params;
  if (!id) return null;

  return data.find((item) => item.course_id === id);
};

export const trimDays = (daysString) => {
  const daysArray = convertDaysToTurkish(daysString)
    .split(",")
    .map((day) => day.trim().substring(0, 10));
  const trimmedDaysString = daysArray.join(",");
  return trimmedDaysString;
};


export const IMG_BASE_URL = VITE_IMAGE_ROOT_PATH;