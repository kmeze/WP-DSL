<script setup>
import {ref, onMounted} from 'vue'
import {useTestPluginStore} from '@/stores/TestPluginStore'

const testPluginStore = useTestPluginStore()
testPluginStore.apiUrl = 'http://localhost:8000'

const credentials = ref({
  userName: '',
  password: '',
})

const rememberMe = ref(true)

onMounted(() => {
  testPluginStore.tryToLogInUser().then((res) => {
        if (res) testPluginStore.fetchMe()
        if (res) testPluginStore.fetchTestEntity()
      }
  )
})
</script>

<template>
  <main>
    <div>
      <h1>Me</h1>
      <div>
        <p>Logged in as: {{ testPluginStore.me.username }}</p>
      </div>
      <div style="margin-bottom: 1rem;">
        <label>Username:</label>
        <input type="text" v-model="credentials.userName">
      </div>
      <div style="margin-bottom: 1rem;">
        <label>Password:</label>
        <input type="password" v-model="credentials.password">
      </div>
      <div style="margin-bottom: 1rem;">
        <input type="checkbox" id="checkbox" v-model="rememberMe"><label>Remeber me</label>
      </div>
      <button type="button" @click="testPluginStore.jwtLogIn(credentials.userName, credentials.password, rememberMe)"
              style="margin-right: 1rem;">Log in
      </button>
      <button type="button" @click="testPluginStore.jwtLogOut()">Log Out</button>
      <p>{{ testPluginStore.authToken }}</p>
    </div>
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
