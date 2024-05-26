const moduleSignalR = {
  state: () => ({
    //signalr
    hub: $.connection.serviceHub,
    insiyatif: {
      datas: [],
    },
    watch: {
      datas: [],
      ping: {
        date: "",
        total: 0,
      },
    },
  }),
  mutations: {
    //signalr
    setInsiyatifData(state, datas) {
      state.insiyatif.datas = datas;
    },
    setWatchData(state, data) {
      console.log("girdi");
      state.watch.datas.unshift(data);
      if (state.watch.datas.length > 20)
        state.watch.datas = state.watch.datas.slice(0, 20);
    },

    setWatchAllDataClear(state) {
      state.watch.datas = [];
    },

    setWatchPing(state, data) {
      state.watch.ping.date = data;
    },
    setWatchRequest(state, data) {
      state.watch.ping.total = data;
    },
  },
  actions: {},
  getters: {
    //signalr

    getHub(state) {
      return state.hub;
    },
    getInsiyatifData(state) {
      return state.insiyatif.datas;
    },

    getWatchDatas(state) {
      return state.watch.datas;
    },

    getWatchPing(state) {
      return state.watch.ping.date;
    },
    getWatchRequest(state) {
      return state.watch.ping.total;
    },
  },
};

export default moduleSignalR;
