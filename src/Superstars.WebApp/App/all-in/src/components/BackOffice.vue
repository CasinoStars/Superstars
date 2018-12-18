<template>
<div class="bo">
      <table style="margin-top:4%;">
      <tr>
        <th>LogID</th>
        <th>UserID</th>
        <th>Action Date </th>
        <th>Action Description</th>
      </tr>
      <tr v-for="(item,index) in logs" :key="index">
          <td> {{item.logId}}</td>
          <td> {{item.userId}}</td>
          <td>{{item.actionDate}}</td>
          <td>{{item.actionDescription}}</td>
      </tr>
    </table>
</div>
</template>

<script>
import { mapGetters, mapActions } from 'vuex';
import BackOfficeApiService from '../services/BackOfficeApiService';
import Vue from 'vue';

export default {

  data(){
    return {
      isAdmin: false,
      logs: {}
    }
  },

     async created(){
      await this.setIsAdmin();
      await this.getLogs();
    },

    async mounted() {
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),
 
    async setIsAdmin() {
      this.isAdmin = await this.executeAsyncRequest(() => BackOfficeApiService.IsAdmin());
    },

    async getLogs() {
      this.logs = await this.executeAsyncRequest(() => BackOfficeApiService.GetLogs());
    }

     }
}
</script>

<style lang="scss">

</style>