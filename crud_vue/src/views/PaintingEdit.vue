<template>
  <Layout :title="id ? 'Редактирование записи' : 'Создание записи'">
    <PaintingForm @submit="onSubmit" :id="id"  />
  </Layout>
</template>

<script>
import { useStore } from 'vuex';

import { updateItem, addItem } from '@/store/paintings/selectors';
import PaintingForm from '@/components/PaintingForm/PaintingForm';
import Layout from '@/components/Layout/Layout';

export default {
  name: 'PaintingEdit',
  props: {
    id: String,
  },
  components: {
    Layout,
    PaintingForm,
  },
  setup() {
    const store = useStore();
    return {
      onSubmit: ({ id, name, img, year, description, id_hall }) => id ?
          updateItem(store, { id, name, img, year, description, id_hall }) :
          addItem(store, { name, img, year, description, id_hall } )
    }
  }

}
</script>

