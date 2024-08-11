<template>
  <Layout title="Картины">
    <PaintingList />
  </Layout>
</template>

<script>
import Layout from '@/components/Layout/Layout';
import PaintingList from '@/components/PaintingList/PaintingList';

export default {
  name: 'Paintings',
  components: {
    Layout,
    PaintingList,
  }
}
</script>
