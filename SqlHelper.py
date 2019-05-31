import pymysql

class SqlHelper:

    def __init__(self):
        # 连接数据库
        self.connect = pymysql.Connect(
            host='cdb-m03voo9x.gz.tencentcdb.com',
            port=10103,
            user='root',
            passwd='gaofeng111',
            db='stockanalysis',
            charset='utf8'
        )
        # 获取游标
        self.cursor = self.connect.cursor()

    def Update(self, sql):
        self.cursor.execute(sql)
        self.connect.commit()
        return cursor.rowcount

    def Serch(self, sql):
        self.cursor.execute(sql)
        return self.cursor.fetchall()

    def Call(self, procedureName, *procedureArgs):
        self.cursor.callproc(procedureName, args= [str(x) for x in procedureArgs])
        self.connect.commit()
        return self.cursor.fetchall()

    def CallWithList(self, procedureName, procedureArgs):
        for arg in procedureArgs:
            self.cursor.callproc(procedureName, args=arg)
        self.connect.commit()
        return self.cursor.fetchall()
