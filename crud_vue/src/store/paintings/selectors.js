import axios from 'axios';

export const fetchItems = async (store) => {
  try {
    const response = await axios.get('http://localhost/server/rest/paintings/list.json');
    store.commit('paintings/setItems', response.data);
  } catch (error) {
    console.error('Error fetching items:', error);
  }
};

export const selectItems = (store) => {
  const { getters } = store;
  return getters['paintings/items']
}

export const removeItem = (store, id) => {
  const { dispatch } = store;
  dispatch('paintings/removeItem', id);
}

export const addItem =  (store, { name, img, year, description, id_hall}) => {
  const { dispatch } = store;
  dispatch('paintings/addItem', { name, img, year, description, id_hall });
}

export const updateItem = (store, { id, name, img, year, description, id_hall }) => {
  const { dispatch } = store;
  dispatch('paintings/updateItem', { id, name, img, year, description, id_hall });
}

export const selectItemById = (store, id) => {
  const { getters } = store;
  return getters['paintings/itemsByKey'][id] || {};
}

export const selectItemsByHallId = async (store, hallId) => {
  try {
    const response = await axios.get(`http://localhost/server/rest/paintings/SelectByID?id_hall=${hallId}`);
    return response.data; // Возвращаем данные без коммита изменений в store
  } catch (error) {
    console.error('Error fetching items:', error);
    return []; // Возвращаем пустой массив в случае ошибки
  }
};

export const fetchItemsByHallId = async (store, hallId) => {
  try {
    const items = await selectItemsByHallId(store, hallId);
    store.commit('paintings/setItemsByHallId', items);
  } catch (error) {
    console.error('Error fetching items by hall ID:', error);
  }
};

