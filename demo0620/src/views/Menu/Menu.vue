<template>
    <div>
        <div >
            <el-button type="primary" @click="getMenuAdd">添加</el-button>  
        </div>

        <el-table :data="tableData" style="width: 100%;margin-bottom: 20px;" row-key="MenuId" border default-expand-all
            :tree-props="{ children: 'children', hasChildren: 'hasChildren' }">
            <el-table-column prop="MenuId" label="菜单ID" sortable width="180">
            </el-table-column>
            <el-table-column prop="MenuName" label="菜单名称" sortable width="180">
            </el-table-column>            
            <el-table-column prop="LinkUrl" label="菜单链接" sortable width="180">
            </el-table-column>

             <el-table-column label="操作">
            <template slot-scope="scope">
                <el-button
                size="mini"
                @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                <el-button
                size="mini"
                type="danger"
                @click="handleDelete(scope.$index, scope.row)">删除</el-button>
            </template>
            </el-table-column>

        </el-table>

        <!-- //添加框 -->
        <el-dialog
            title="提示"
            :visible.sync="dialogVisible"
            width="30%"
            >
            <HelloWorld @Add=Add  />
        </el-dialog>
         <!-- /修改框 -->
        <el-dialog
            title="提示"
            :visible.sync="openEdit"
            width="30%"
            >
            <HellEdit @Edit=Edit :formdate=form />
        </el-dialog>
    </div>
</template>

<script>
    import HelloWorld from './MenuAdd.vue'
    import HellEdit from './MenuEdit.vue'

    export default {
        name: 'RbacUIMenu',
        components:{
            HelloWorld,
            HellEdit
        },
        data() {
            return {
                tableData: [],
                 defaultProps: {
                children: 'children',
                label: 'MenuName'
                },
                dialogVisible:false,
                openEdit:false,
                form:{
                    tableData:[],
                    MenuId:'',
                    MenuName:'',
                    LinkUrl:'',
                }
            };
        },
        methods: {
            getMenu(){
                debugger;
                this.$axios.get("/api/Menu/GetMenuTreeList").then(
                    res=>{
                        console.log(res);
                     this.tableData = res.data;
              })
            },
           
             handleNodeClick(data) {
                console.log(data);
            },
            //添加节点
            getMenuAdd(){
                this.dialogVisible=true;
            },
            Add(){
                this.dialogVisible=false;
                this.getMenu();
            },
            handleEdit(index, row) {
                this.form.tableData=this.tableData;
                this.form=row;
                this.openEdit=true;
            },
            Edit(){
                this.openEdit=false;
                this.getMenu();
            },
            handleDelete(index, row) {
                this.$axios.post("/api/Menu/GetMenuDel",row).then(
                    res=>{
                        debugger;
                     if(res.data>0)
                     {
                        this.$message.success("删除成功");
                        this.getMenu();
                     }
                     else
                     {
                        this.$message.error("不是最底层菜单");
                     }
              })
            }
        },
        created(){
            this.getMenu();
        }
    };
</script>
