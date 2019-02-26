<template>
<div class="tab">
  <h2 class="title">日志表</h2>
  <div class="logs">
    <el-tabs tab-position="left" >
      <el-tab-pane v-if="logList && logList.length" v-for="(log,index) in logList"  :label="log.name" >
        <div v-if="log.content && log.content.length > 0" v-for="content in log.content">{{content}}</div>
      </el-tab-pane>
      <!--<el-tab-pane label="配置管理">配置管理</el-tab-pane>-->
      <!--<el-tab-pane label="角色管理">角色管理</el-tab-pane>-->
      <!--<el-tab-pane label="定时任务补偿">定时任务补偿</el-tab-pane>-->
    </el-tabs>
  </div>
</div>
</template>

<script>
  let _this;
  import $ from 'jquery'
export default {
  name: 'HelloWorld',
  data() {
    return {
      value: "",
      logList:[],
    }
  },

  mounted(){
    _this = this;
    const connection = new this.signalr.HubConnectionBuilder()
      .withUrl("http://localhost:19222/pushLogHub")
      .build();

    connection.on('ConnectionSuccess', function (message) {
      console.log("ConnectionSuccess and id:", message);
    })

    connection.on('ReciveMessage', function (message) {
      if(_this.logList && _this.logList.length > 0){
        let index = _this.logList.findIndex(function(item){
         return item.name == message.clientName;
        });
        if(index > -1){
          _this.logList[index].content.push(message.content);
        }else{
          _this.logList.push({id:message.appId, name:message.clientName, content:[message.content]});
        }
      }else{
        _this.logList.push({id:message.appId, name:message.clientName, content:[message.content]});
      }
    });
    connection.serverTimeoutInMilliseconds = 15000; // 100 second

    connection.start();
  },
  methods:{

  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
@import "../assets/css/index.css";
</style>
<style>
  .tab .el-tabs--left .el-tabs__nav.is-left{
    margin-left: 15px!important;
  }
  .tab .el-tabs--left .el-tabs__item.is-left{
    text-align: center!important;
  }
  .tab .title{
    margin-bottom: 65px;
  }
  .logs {
    height: 412px!important;
  }
  .logs .el-tabs__content{
    height: 412px;
    overflow: auto!important;
  }
</style>
