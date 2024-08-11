import axios from 'axios';

export const fetchItems = async ( store ) => {
  await new Promise(resolve => setTimeout(resolve, 30));
  try {
    const response = await axios.get('http://localhost/server/rest/halls/list.json');

    store.commit('halls/setItems', response.data);
  } catch (error) {
    console.error('Error fetching items:', error);
  }
};

export const selectItems = ( store ) => {
  const { getters } = store;
  return getters['halls/items']
}

export const removeItem = ( store, id ) => {
  const { dispatch } = store;
  dispatch('halls/removeItem', id);
}

export const addItem = ( store, { name } ) => {
  const { dispatch } = store;
  dispatch('halls/addItem', { name });
}

export const updateItem = ( store, { id, name}) => {
  const { dispatch } = store;
  dispatch('halls/updateItem', { id, name});
}

export const selectItemById = (store, id) => {
  const { getters } = store;
  return getters['halls/itemsByKey'][id] || {};
}
