<script setup>
import {useTestPluginStore} from '@/stores/TestPluginStore'

const testPluginStore = useTestPluginStore()
testPluginStore.apiUrl = 'http://localhost:8000/wp-json/TestPlugin/v1/TestEntity'

</script>

<template>
  <main>
    <h1>TestEntity Count: {{ testPluginStore.TestEntity.length }}</h1>
    <button type="button" @click="testPluginStore.fetchTestEntity()">Fetch</button>
    <ul>
      <li v-for="item in testPluginStore.TestEntity">
        {{ item }}
      </li>
    </ul>
  </main>
</template>

<style scoped>
</style>
