<template>
<div class="bo">
      <div style="text-align: center;margin-top: 2%;font-family: 'Courier New', sans-serif;">
      <h1 style="font-variant: small-caps; font-size: 45px;">
        <i class="fa fa-chevron-left" @click="SwapActionsOrUsers()" id="chevron"></i>
        <strong>Back Office </strong>
        <i class="fa fa-chevron-right" @click="SwapActionsOrUsers()" id="chevron"></i>
      </h1>
      </div>

    <center>
      <div v-if="ActionsOrUser == 'Action'" id="blueCircle">
        <div v-if="ActionsOrUser == 'Action'" style="margin-top: -2px; margin-left:150%;" id="whiteCircle"></div>
      </div>
      <div v-if="ActionsOrUser !== 'Action'" id="whiteCircle">
        <div v-if="ActionsOrUser !== 'Action'" style="margin-top: -2px; margin-left:150%;" id="blueCircle"></div>
      </div>
    </center>

      <table style="margin-top:3%;" id="actionTable">
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
    <table style="margin-top:3%;" id="usersTable">
      <thead>
        <tr>
          <th>Pseudo</th>
          <th>Action</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(e, index) in usersNames" :key='index'>
          <td v-if="usersNames[index] != myPseudo"><a class="link" :href="'statistics?pseudo='+usersNames[index]">{{usersNames[index]}}</a></td>
          <td v-if="usersNames[index] != myPseudo"><i class="fa fa-user-times" @click="DeleteUser(usersNames[index])"> </i></td>
        </tr>
      </tbody>
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
      logs: {},
      ActionsOrUser : 'Action',
      usersNames: [],
      myPseudo: '',
      TrueOrFake: true
    }
  },

     async created(){
      this.setIsAdmin();
      await this.getLogs();
      await this.getUsers();
      this.myPseudo = UserApiService.pseudo;
      var tab = document.getElementById('actionTable');
      tab.style.display = "table";
      var tab2 = document.getElementById('usersTable');
      tab2.style.display = "none";
    },

    async mounted() {
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),
 
    setIsAdmin() {
      this.isAdmin = UserApiService.isAdmin;
    },

    async getLogs() {
      this.logs = await this.executeAsyncRequest(() => BackOfficeApiService.GetLogs());
    },

    async getUsers() {
      this.usersNames = await this.executeAsyncRequest(() => BackOfficeApiService.GetUsers());
    },

    async DeleteUser(name) {
      await this.executeAsyncRequest(() => BackOfficeApiService.DeleteUser(name));
      await this.getUsers();
    },

    async SwapActionsOrUsers() {
      if(this.ActionsOrUser == 'Action') {
        this.ActionsOrUser = 'User';
        var tab = document.getElementById('actionTable');
        tab.style.display = "none";
        var tab2 = document.getElementById('usersTable');
        tab2.style.display = "table";
      } else {
        this.ActionsOrUser = 'Action';
        var tab = document.getElementById('actionTable');
        tab.style.display = "table";
        var tab2 = document.getElementById('usersTable');
        tab2.style.display = "none";
      }
    }

     }
}
</script>

<style lang="scss">

    .bo #blueCircle {
    background: #f1f3f3;
    border-radius: 40%;
    width: 13px;
    height: 13px;
    border: 2px solid #0e97d7;
  }

  .bo #whiteCircle {
    background: #f1f3f3;
    border-radius: 40%;
    width: 13px;
    height: 13px;
    border: 2px solid #81888b;
  }

  .bo #chevron {
    color: gray;
    font-size: 50%;
  }

  .bo #chevron:hover {
    cursor: pointer;
    opacity: 0.5;
  }

  .bo a {
      color: black;
    }

  .bo a:hover {
      color: grey;
      font-size: 120%;
    }

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

    .bo i:hover {
      cursor: pointer;
      color: red;
      font-size: 120%;
    }
</style>