using Bootstrapper.Core.nCore;
using Toygar.DB.Data.nDataService;
using Toygar.DB.Data.nDataService.nDatabase.nQuery;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Toygar.DB.Data.nDataService.nDatabase.nSql;
using Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nEntityServices.nEntities;
using Toygar.DB.Data.nDataService.nDatabase.nEntity;
using Toygar.DB.Data.nDataService.nDatabase.nEntity.nAttributes;
using System.Collections;

namespace Toygar.DB.Data.nDataServiceManager.nGlobalDataServices.nDataManager
{
    public class cProfileDataManager : cBaseDataManager
    {
        public cProfileDataManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager)
          : base(_CoreServiceContext, _DataServiceManager)
        {
        }


        public cProfileEntity GetProfileByHostName(string _HostName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();
            cProfileEntity __Result = __DataService.Database.Query<cProfileEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.HostName).Eq(_HostName)
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }

        public cProfileEntity GetProfileByEntityTypeAndHostName<TServiceBaseEntity>(string _HostName)
            where TServiceBaseEntity : cBaseEntity
        {
            return GetProfileByEntityTypeAndHostName(_HostName, typeof(TServiceBaseEntity).FullName);
        }

        public cProfileEntity GetProfileByEntityTypeAndHostName(string _HostName, string _FullEntityTypeName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cProfileEntity __ProfileEntityAlias = null;

            cProfileEntity __Result = __DataService.Database.Query(() => __ProfileEntityAlias)
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.HostName).Eq(_HostName)
                .And
                .Exists(
                    __DataService.Database.Query<cDBSettingEntity>()
                    .SelectID()
                    .Where()
                    .Operand<cProfileEntity>().Eq(() => __ProfileEntityAlias.ID)
                    .And
                    .Operand(__Item => __Item.EntityType).Eq(_FullEntityTypeName)
                    .ToQuery()
                )
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }

        public dynamic GetProfileWithDBSettingByEntityTypeAndHostName<TServiceBaseEntity>(string _HostName)
            where TServiceBaseEntity : cBaseEntity
        {
            return GetProfileWithDBSettingByEntityTypeAndHostName(_HostName, typeof(TServiceBaseEntity).FullName);
        }

        public dynamic GetProfileWithDBSettingByEntityTypeAndHostName(string _HostName, string _FullEntityTypeName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cProfileEntity __ProfileEntityAlias = null;
            cDBSettingEntity __DBSettingEntityAlias = null;

            cQuery<cProfileEntity> __Query = __DataService.Database.Query(() => __ProfileEntityAlias)
                .SelectAliasAllColumns(() => __ProfileEntityAlias)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.Server)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.UserId)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.Password)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.DBName)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.MaxConnectionCount)
                .SelectAliasColumn(() => __DBSettingEntityAlias, __Item => __Item.EntityType)

                .Inner<cDBSettingEntity>().Join(() => __DBSettingEntityAlias)
                        .On()
                        .Operand<cProfileEntity>().Eq(() => __ProfileEntityAlias.ID)
                        .ToQuery()

                .Where()
                .Operand(__Item => __Item.HostName).Eq(_HostName)
                .And
                .Exists(
                    __DataService.Database.Query<cDBSettingEntity>()
                    .SelectID()
                    .Where()
                    .Operand<cProfileEntity>().Eq(() => __ProfileEntityAlias.ID)
                    .And
                    .Operand(__Item => __Item.EntityType).Eq(_FullEntityTypeName)
                    .ToQuery()
                )
                .ToQuery();

            return __Query.ToDynamicObjectList().FirstOrDefault();
        }

        public cDBSettingEntity GetDBSettingByEntityTypeAndHostName<TServiceBaseEntity>(string _HostName)
           where TServiceBaseEntity : cBaseEntity
        {
            return GetDBSettingByEntityTypeAndHostName(_HostName, typeof(TServiceBaseEntity).FullName);
        }

        public cDBSettingEntity GetDBSettingByEntityTypeAndHostName(string _HostName, string _FullEntityTypeName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cDBSettingEntity __DBSettingEntityAlias = null;

            cDBSettingEntity __Result = __DataService.Database.Query(() => __DBSettingEntityAlias)
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.EntityType).Eq(_FullEntityTypeName)
                .And
                .Exists(
                    __DataService.Database.Query<cProfileEntity>()
                    .SelectID()
                    .Where()
                    .Operand(__Item => __Item.ID).Eq<cProfileEntity>(() => __DBSettingEntityAlias)
                    .And
                    .Operand(__Item => __Item.HostName).Eq(_HostName)
                    .ToQuery()
                )
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }


        public cProfileEntity GetProfileCreateIfNotExists(
            string _HostName
            , string _EntityType
            )
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cProfileEntity __ProfileEntity = GetProfileByEntityTypeAndHostName(_HostName, _EntityType);
            if (__ProfileEntity == null)
            {
                __ProfileEntity = __DataService.Database.CreateNew<cProfileEntity>();
                __ProfileEntity.HostName = _HostName;
                __ProfileEntity.Save();
            }
            return __ProfileEntity;
        }

        public cDBSettingEntity GetDBSettingCreateIfNotExists(
           cProfileEntity _ProfileEntity
           , string _EntityType
           , string _UserId
           , string _Password
           , string _Server
           , string _DBName
           , int _DBMaxConnectionCount
           )
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cDBSettingEntity __DBSettingEntity = GetDBSettingByEntityTypeAndHostName(_ProfileEntity.HostName, _EntityType);
            if (__DBSettingEntity == null)
            {
                __DBSettingEntity = __DataService.Database.CreateNew<cDBSettingEntity>();
                __DBSettingEntity.EntityType = _EntityType;
                __DBSettingEntity.UserId = _UserId;
                __DBSettingEntity.Password = _Password;
                __DBSettingEntity.Server = _Server;
                __DBSettingEntity.DBName = _DBName;
                __DBSettingEntity.MaxConnectionCount = _DBMaxConnectionCount;
                __DBSettingEntity.Save(_ProfileEntity);
            }
            return __DBSettingEntity;
        }

        public void CreateDBSettingInProfile(
            cProfileEntity _ProfileEntity
            , string _DBUserId
            , string _DBPassword
            , string _DBServer
            , string _DBName
            , int _DBMaxConnectionCount
            , string _EntityType
            )
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cDBSettingEntity __DBSettingEntity = __DataService.Database.CreateNew<cDBSettingEntity>();
            __DBSettingEntity.Password = _DBPassword;
            __DBSettingEntity.UserId = _DBUserId;
            __DBSettingEntity.Server = _DBServer;
            __DBSettingEntity.DBName = _DBName;
            __DBSettingEntity.MaxConnectionCount = _DBMaxConnectionCount;
            __DBSettingEntity.EntityType = _EntityType;
            __DBSettingEntity.Save(_ProfileEntity);

        }
    }
}
