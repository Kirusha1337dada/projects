<template>
  <Layout title="Залы">
    <HallList />
  </Layout>
</template>

<script>
import Layout from '@/components/Layout/Layout';
import HallList from '@/components/HallList/HallList';

export default {
  name: 'HallsPage',
  components: {
    Layout,
    HallList,
  }
}
</script>
