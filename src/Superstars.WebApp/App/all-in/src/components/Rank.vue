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
    <div v-for="(e,index) of pseudos" :key='index'>
       <td>{{e}}</td>
    </div>
    <div v-for="(e,index) of profits" :key='index'>
       <td>{{e}}</td>
    </div>
    <td>{{playertrueprofit}}</td>
    <td>{{playernbgames}}</td>
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
import RankApiService from '../services/RankApiServices';


export default {
    data() {
        return {
            pseudos: {},
            profits: {},
            playertrueprofit: 0,
            playerfakeprofit: 0,
            playernbgames: 0
        }
    },    

    async mounted() {
      // this.handvalue = await this.executeAsyncRequest(() => BlackJackApiService.getplayerHandValue());
      this.pseudos = await this.executeAsyncRequest(() => RankApiService.GetPseudoList());
      for(var i = 0;i<console.log(this.pseudos.length);i++ )
      {
        var k = await this.executeAsyncRequest(()=> RankApiService.GetPlayerProfit());
        this.profits.push(k);
      } 

    },

     methods: {
    ...mapActions(['executeAsyncRequest']),


     }
}
</script>

 <style>
 
 </style>
 
