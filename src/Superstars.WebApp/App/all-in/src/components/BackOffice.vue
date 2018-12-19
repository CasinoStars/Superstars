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
import UserApiService from '../services/UserApiService';

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
      this.isAdmin = UserApiService.isAdmin;
    },

    async getLogs() {
      this.logs = await this.executeAsyncRequest(() => BackOfficeApiService.GetLogs());
    }

     }
}
</script>

<style lang="scss">
    .bo tr {
        background-color: #f2f2f2;       
    }
    
    .bo table {
      width: 100%;
    }

.bo td, th {
        border-bottom: 1px solid #dddddd;
        text-align: left;
        padding: 14px;
        font-family: 'Courier New', sans-serif;
        font-size: 21px;
        font-variant: small-caps;
        font-weight: bold;
    }

.bo th {
        background-color: #343a40;
        color: white;
}
</style>