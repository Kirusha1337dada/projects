//import { fetchItems } from '../halls/selectors';
import api from './api';
//import { fetchItems } from './selectors';

export default {
  namespaced: true,
  state: {
    items: [],
    itemsByHallId: []
  },
  getters: {
    items: state => state.items,
    itemsByKey: state => state.items.reduce((res, cur) => {
      res[cur['id']] = cur;
      return res;
    }, {}),
  },
  mutations: {
    setItemsByHallId: (state, items) => {
      state.itemsByHallId = items;
    },
    setItems: (state, items) => {
      state.items = items;
      console.log(items);
    },
    setItem: (state, item) => {
      state.items.push(item);
    },
    removeItem: (state, idRemove) => {
      state.items = state.items.filter(({ id }) => id !== idRemove)
    },
    updateItem: (state, updateItem) => {
      const index = state.items.findIndex(item => +item.id === +updateItem.id);
      state.items[index] = updateItem;
    },
    itemsById: (state, idItem) =>{
      state.items = state.items.filter(({ id_hall }) => id_hall !== idItem);
    }
  },
  actions: {
    fetchItems: async ({ commit }) => {
      const response = await api.paintings();
      const items = await response.json();
      commit('setItems', items)
    },
    removeItem: async ({ commit }, id) => {
      const idRemovedItem = await api.remove( id );
      commit('removeItem', idRemovedItem);

    },
    fetchItemsByHallId: async ({ commit }, hallId) => {
      try {
        const items = await api.paintingId(hallId);
        commit('setItemsByHallId', items);
      } catch (error) {
        console.error('Error fetching items by hall ID:', error);
      }
    },
    addItem: async ({ commit }, { name, img, year, description, id_hall}) => {
      const item = await api.add({ name, img, year, description, id_hall })
      commit('setItem', item)
    },
    updateItem: async ({ commit }, { id, name, img, year, description, id_hall }) => {
      const item = await api.update({ id, name, img, year, description, id_hall });
      commit('updateItem', item);
    }
  },
}
