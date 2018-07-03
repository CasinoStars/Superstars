<template>
    <div>
        <div style="text-align: center;margin-top 2%;font-family: 'Courier New', sans-serif;">
          <h1 style="font-variant: small-caps;"> <strong> Mes statistiques  </strong></h1>
        </div>
  <br><br>
<table>
  <tr>
    <th>Pseudo</th>
    <th>Profit</th>
    <th>Nombre de parties</th>
  </tr>
  <tr>
    <th>
    <div v-for="(e,index) of pseudos" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
    <th>
    <div v-for="(e,index) of profits" :key='index'>
       <td>{{e}}</td>
    </div>
    </th>
    <th>
      <div v-for="(e,index) of playernbgames" :key='index'>
        <td>{{e}}</td>
      </div>
    </th>
  </tr>
</table>
 <br>
 <br>
 <br>
 <br>
 <br>

</div>
</template>

<script>

import { mapActions } from 'vuex';
import Vue from 'vue';
import GameApiService from '../services/GameApiService';
import WalletApiService from '../services/WalletApiService';
import UserApiService from '../services/UserApiService';
import RankApiService from '../services/RankApiService';


export default {
    data() {
        return {
            pseudos: {},
            profits: {},
            playernbgames: {},
        }
    },    

    async mounted() {
      // this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
      this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPlayersUserNameSorted());
      this.profits = await this.executeAsyncRequest(() => RankApiService.GetPlayersProfitSorted());
      for (var i = 0; i < console.log(clothing.length); i++) 
      {
         playernbgames[i] = await this.executeAsyncRequest(()=> RankApiService.GetPlayerNumberParts());
      }
    },

     methods: {
    ...mapActions(['executeAsyncRequest']),


     }
}
</script>

 <style>
 
 </style>
 
