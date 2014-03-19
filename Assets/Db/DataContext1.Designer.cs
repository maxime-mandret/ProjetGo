﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using LinqConnect template.
// Code is generated on: 19/03/2014 04:21:54
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using Devart.Data.Linq;
using Devart.Data.Linq.Mapping;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;

namespace Assets.Db
{

    [DatabaseAttribute(Name = "goban")]
    [ProviderAttribute(typeof(Devart.Data.MySql.Linq.Provider.MySqlDataProvider))]
    public partial class DbGobansDataContext : Devart.Data.Linq.DataContext
    {
        public static CompiledQueryCache compiledQueryCache = CompiledQueryCache.RegisterDataContext(typeof(DbGobansDataContext));
        private static MappingSource mappingSource = new Devart.Data.Linq.Mapping.AttributeMappingSource();

        #region Extensibility Method Definitions
    
        partial void OnCreated();
        partial void OnSubmitError(Devart.Data.Linq.SubmitErrorEventArgs args);
        partial void InsertDbCoup(DbCoup instance);
        partial void UpdateDbCoup(DbCoup instance);
        partial void DeleteDbCoup(DbCoup instance);
        partial void InsertDbJoueur(DbJoueur instance);
        partial void UpdateDbJoueur(DbJoueur instance);
        partial void DeleteDbJoueur(DbJoueur instance);
        partial void InsertDbPartie(DbPartie instance);
        partial void UpdateDbPartie(DbPartie instance);
        partial void DeleteDbPartie(DbPartie instance);

        #endregion

        public DbGobansDataContext() :
        base(GetConnectionString("DbGobansDataContextConnectionString"), mappingSource)
        {
            OnCreated();
        }

        public DbGobansDataContext(MappingSource mappingSource) :
        base(GetConnectionString("DbGobansDataContextConnectionString"), mappingSource)
        {
            OnCreated();
        }

        private static string GetConnectionString(string connectionStringName)
        {
            System.Configuration.ConnectionStringSettings connectionStringSettings = System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName];
            if (connectionStringSettings == null)
                throw new InvalidOperationException("Connection string \"" + connectionStringName +"\" could not be found in the configuration file.");
            return connectionStringSettings.ConnectionString;
        }

        public DbGobansDataContext(string connection) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public DbGobansDataContext(System.Data.IDbConnection connection) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public DbGobansDataContext(string connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public DbGobansDataContext(System.Data.IDbConnection connection, MappingSource mappingSource) :
            base(connection, mappingSource)
        {
          OnCreated();
        }

        public Devart.Data.Linq.Table<DbCoup> DbCoups
        {
            get
            {
                return this.GetTable<DbCoup>();
            }
        }

        public Devart.Data.Linq.Table<DbJoueur> DbJoueurs
        {
            get
            {
                return this.GetTable<DbJoueur>();
            }
        }

        public Devart.Data.Linq.Table<DbPartie> DbParties
        {
            get
            {
                return this.GetTable<DbPartie>();
            }
        }
    }
}

namespace Assets.Db
{

    /// <summary>
    /// There are no comments for Assets.Db.DbCoup in the schema.
    /// </summary>
    [Table(Name = @"goban.coup")]
    public partial class DbCoup : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private long _IdCoup;

        private long _IdPartie;

        private System.DateTime _HeureCoup;

        private long _IdJoueur;

        private System.Nullable<int> _X;

        private System.Nullable<int> _Y;
        #pragma warning restore 0649

        private EntityRef<DbPartie> _DbPartie;
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnIdCoupChanging(long value);
        partial void OnIdCoupChanged();
        partial void OnIdPartieChanging(long value);
        partial void OnIdPartieChanged();
        partial void OnHeureCoupChanging(System.DateTime value);
        partial void OnHeureCoupChanged();
        partial void OnIdJoueurChanging(long value);
        partial void OnIdJoueurChanged();
        partial void OnXChanging(System.Nullable<int> value);
        partial void OnXChanged();
        partial void OnYChanging(System.Nullable<int> value);
        partial void OnYChanged();
        #endregion

        public DbCoup()
        {
            this._DbPartie  = default(EntityRef<DbPartie>);
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for IdCoup in the schema.
        /// </summary>
        [Column(Name = @"idCoup", Storage = "_IdCoup", AutoSync = AutoSync.OnInsert, CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL AUTO_INCREMENT", IsDbGenerated = true, IsPrimaryKey = true)]
        public long IdCoup
        {
            get
            {
                return this._IdCoup;
            }
            set
            {
                if (this._IdCoup != value)
                {
                    this.OnIdCoupChanging(value);
                    this.SendPropertyChanging();
                    this._IdCoup = value;
                    this.SendPropertyChanged("IdCoup");
                    this.OnIdCoupChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IdPartie in the schema.
        /// </summary>
        [Column(Name = @"idPartie", Storage = "_IdPartie", CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public long IdPartie
        {
            get
            {
                return this._IdPartie;
            }
            set
            {
                if (this._IdPartie != value)
                {
                    if (this._DbPartie.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    this.OnIdPartieChanging(value);
                    this.SendPropertyChanging();
                    this._IdPartie = value;
                    this.SendPropertyChanged("IdPartie");
                    this.OnIdPartieChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for HeureCoup in the schema.
        /// </summary>
        [Column(Name = @"heureCoup", Storage = "_HeureCoup", CanBeNull = false, DbType = "DATETIME NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public System.DateTime HeureCoup
        {
            get
            {
                return this._HeureCoup;
            }
            set
            {
                if (this._HeureCoup != value)
                {
                    this.OnHeureCoupChanging(value);
                    this.SendPropertyChanging();
                    this._HeureCoup = value;
                    this.SendPropertyChanged("HeureCoup");
                    this.OnHeureCoupChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IdJoueur in the schema.
        /// </summary>
        [Column(Name = @"idJoueur", Storage = "_IdJoueur", CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public long IdJoueur
        {
            get
            {
                return this._IdJoueur;
            }
            set
            {
                if (this._IdJoueur != value)
                {
                    this.OnIdJoueurChanging(value);
                    this.SendPropertyChanging();
                    this._IdJoueur = value;
                    this.SendPropertyChanged("IdJoueur");
                    this.OnIdJoueurChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for X in the schema.
        /// </summary>
        [Column(Name = @"x", Storage = "_X", DbType = "INT(2) NULL", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> X
        {
            get
            {
                return this._X;
            }
            set
            {
                if (this._X != value)
                {
                    this.OnXChanging(value);
                    this.SendPropertyChanging();
                    this._X = value;
                    this.SendPropertyChanged("X");
                    this.OnXChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Y in the schema.
        /// </summary>
        [Column(Name = @"y", Storage = "_Y", DbType = "INT(2) NULL", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<int> Y
        {
            get
            {
                return this._Y;
            }
            set
            {
                if (this._Y != value)
                {
                    this.OnYChanging(value);
                    this.SendPropertyChanging();
                    this._Y = value;
                    this.SendPropertyChanged("Y");
                    this.OnYChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DbPartie in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbPartie_DbCoup", Storage="_DbPartie", ThisKey="IdPartie", OtherKey="IdPartie", IsForeignKey=true)]
        public DbPartie DbPartie
        {
            get
            {
                return this._DbPartie.Entity;
            }
            set
            {
                DbPartie previousValue = this._DbPartie.Entity;
                if ((previousValue != value) || (this._DbPartie.HasLoadedOrAssignedValue == false))
                {
                    this.SendPropertyChanging();
                    if (previousValue != null)
                    {
                        this._DbPartie.Entity = null;
                        previousValue.DbCoups.Remove(this);
                    }
                    this._DbPartie.Entity = value;
                    if (value != null)
                    {
                        this._IdPartie = value.IdPartie;
                        value.DbCoups.Add(this);
                    }
                    else
                    {
                        this._IdPartie = default(long);
                    }
                    this.SendPropertyChanged("DbPartie");
                }
            }
        }
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {    
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {    
		        var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// There are no comments for Assets.Db.DbJoueur in the schema.
    /// </summary>
    [Table(Name = @"goban.joueurs")]
    public partial class DbJoueur : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private long _IdJoueur;

        private string _Nom;
        #pragma warning restore 0649

        private EntitySet<DbPartie> _DbParties_IdJoueurBlanc;

        private EntitySet<DbPartie> _DbParties_IdJoueurNoir;
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnIdJoueurChanging(long value);
        partial void OnIdJoueurChanged();
        partial void OnNomChanging(string value);
        partial void OnNomChanged();
        #endregion

        public DbJoueur()
        {
            this._DbParties_IdJoueurBlanc = new EntitySet<DbPartie>(new Action<DbPartie>(this.attach_DbParties_IdJoueurBlanc), new Action<DbPartie>(this.detach_DbParties_IdJoueurBlanc));
            this._DbParties_IdJoueurNoir = new EntitySet<DbPartie>(new Action<DbPartie>(this.attach_DbParties_IdJoueurNoir), new Action<DbPartie>(this.detach_DbParties_IdJoueurNoir));
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for IdJoueur in the schema.
        /// </summary>
        [Column(Name = @"idJoueur", Storage = "_IdJoueur", AutoSync = AutoSync.OnInsert, CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL AUTO_INCREMENT", IsDbGenerated = true, IsPrimaryKey = true)]
        public long IdJoueur
        {
            get
            {
                return this._IdJoueur;
            }
            set
            {
                if (this._IdJoueur != value)
                {
                    this.OnIdJoueurChanging(value);
                    this.SendPropertyChanging();
                    this._IdJoueur = value;
                    this.SendPropertyChanged("IdJoueur");
                    this.OnIdJoueurChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for Nom in the schema.
        /// </summary>
        [Column(Name = @"nom", Storage = "_Nom", CanBeNull = false, DbType = "VARCHAR(20) NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public string Nom
        {
            get
            {
                return this._Nom;
            }
            set
            {
                if (this._Nom != value)
                {
                    this.OnNomChanging(value);
                    this.SendPropertyChanging();
                    this._Nom = value;
                    this.SendPropertyChanged("Nom");
                    this.OnNomChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DbParties_IdJoueurBlanc in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbJoueur_DbPartie", Storage="_DbParties_IdJoueurBlanc", ThisKey="IdJoueur", OtherKey="IdJoueurBlanc", DeleteRule="NO ACTION")]
        public EntitySet<DbPartie> DbParties_IdJoueurBlanc
        {
            get
            {
                return this._DbParties_IdJoueurBlanc;
            }
            set
            {
                this._DbParties_IdJoueurBlanc.Assign(value);
            }
        }

    
        /// <summary>
        /// There are no comments for DbParties_IdJoueurNoir in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbJoueur_DbPartie1", Storage="_DbParties_IdJoueurNoir", ThisKey="IdJoueur", OtherKey="IdJoueurNoir", DeleteRule="NO ACTION")]
        public EntitySet<DbPartie> DbParties_IdJoueurNoir
        {
            get
            {
                return this._DbParties_IdJoueurNoir;
            }
            set
            {
                this._DbParties_IdJoueurNoir.Assign(value);
            }
        }
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {    
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {    
		        var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void attach_DbParties_IdJoueurBlanc(DbPartie entity)
        {
            this.SendPropertyChanging("DbParties_IdJoueurBlanc");
            entity.DbJoueurs_IdJoueurBlanc = this;
        }
    
        private void detach_DbParties_IdJoueurBlanc(DbPartie entity)
        {
            this.SendPropertyChanging("DbParties_IdJoueurBlanc");
            entity.DbJoueurs_IdJoueurBlanc = null;
        }

        private void attach_DbParties_IdJoueurNoir(DbPartie entity)
        {
            this.SendPropertyChanging("DbParties_IdJoueurNoir");
            entity.DbJoueurs_IdJoueurNoir = this;
        }
    
        private void detach_DbParties_IdJoueurNoir(DbPartie entity)
        {
            this.SendPropertyChanging("DbParties_IdJoueurNoir");
            entity.DbJoueurs_IdJoueurNoir = null;
        }
    }

    /// <summary>
    /// There are no comments for Assets.Db.DbPartie in the schema.
    /// </summary>
    [Table(Name = @"goban.partie")]
    public partial class DbPartie : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(System.String.Empty);
        #pragma warning disable 0649

        private long _IdPartie;

        private long _IdJoueurNoir;

        private System.Nullable<long> _IdJoueurBlanc;

        private string _EtatPartie = @"playing";

        private System.DateTime _HeureDebut = DateTime.Now;

        private System.Nullable<System.DateTime> _HeureFin;
        #pragma warning restore 0649

        private EntitySet<DbCoup> _DbCoups;

        private EntityRef<DbJoueur> _DbJoueurs_IdJoueurBlanc;

        private EntityRef<DbJoueur> _DbJoueurs_IdJoueurNoir;
    
        #region Extensibility Method Definitions

        partial void OnLoaded();
        partial void OnValidate(ChangeAction action);
        partial void OnCreated();
        partial void OnIdPartieChanging(long value);
        partial void OnIdPartieChanged();
        partial void OnIdJoueurNoirChanging(long value);
        partial void OnIdJoueurNoirChanged();
        partial void OnIdJoueurBlancChanging(System.Nullable<long> value);
        partial void OnIdJoueurBlancChanged();
        partial void OnEtatPartieChanging(string value);
        partial void OnEtatPartieChanged();
        partial void OnHeureDebutChanging(System.DateTime value);
        partial void OnHeureDebutChanged();
        partial void OnHeureFinChanging(System.Nullable<System.DateTime> value);
        partial void OnHeureFinChanged();
        #endregion

        public DbPartie()
        {
            this._DbCoups = new EntitySet<DbCoup>(new Action<DbCoup>(this.attach_DbCoups), new Action<DbCoup>(this.detach_DbCoups));
            this._DbJoueurs_IdJoueurBlanc  = default(EntityRef<DbJoueur>);
            this._DbJoueurs_IdJoueurNoir  = default(EntityRef<DbJoueur>);
            OnCreated();
        }

    
        /// <summary>
        /// There are no comments for IdPartie in the schema.
        /// </summary>
        [Column(Name = @"idPartie", Storage = "_IdPartie", AutoSync = AutoSync.OnInsert, CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL AUTO_INCREMENT", IsDbGenerated = true, IsPrimaryKey = true)]
        public long IdPartie
        {
            get
            {
                return this._IdPartie;
            }
            set
            {
                if (this._IdPartie != value)
                {
                    this.OnIdPartieChanging(value);
                    this.SendPropertyChanging();
                    this._IdPartie = value;
                    this.SendPropertyChanged("IdPartie");
                    this.OnIdPartieChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IdJoueurNoir in the schema.
        /// </summary>
        [Column(Name = @"idJoueurNoir", Storage = "_IdJoueurNoir", CanBeNull = false, DbType = "INT(10) UNSIGNED NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public long IdJoueurNoir
        {
            get
            {
                return this._IdJoueurNoir;
            }
            set
            {
                if (this._IdJoueurNoir != value)
                {
                    if (this._DbJoueurs_IdJoueurNoir.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    this.OnIdJoueurNoirChanging(value);
                    this.SendPropertyChanging();
                    this._IdJoueurNoir = value;
                    this.SendPropertyChanged("IdJoueurNoir");
                    this.OnIdJoueurNoirChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for IdJoueurBlanc in the schema.
        /// </summary>
        [Column(Name = @"idJoueurBlanc", Storage = "_IdJoueurBlanc", DbType = "INT(10) UNSIGNED NULL", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<long> IdJoueurBlanc
        {
            get
            {
                return this._IdJoueurBlanc;
            }
            set
            {
                if (this._IdJoueurBlanc != value)
                {
                    if (this._DbJoueurs_IdJoueurBlanc.HasLoadedOrAssignedValue)
                    {
                        throw new ForeignKeyReferenceAlreadyHasValueException();
                    }

                    this.OnIdJoueurBlancChanging(value);
                    this.SendPropertyChanging();
                    this._IdJoueurBlanc = value;
                    this.SendPropertyChanged("IdJoueurBlanc");
                    this.OnIdJoueurBlancChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for EtatPartie in the schema.
        /// </summary>
        [Column(Name = @"etatPartie", Storage = "_EtatPartie", DbType = "ENUM NULL", UpdateCheck = UpdateCheck.Never)]
        public string EtatPartie
        {
            get
            {
                return this._EtatPartie;
            }
            set
            {
                if (this._EtatPartie != value)
                {
                    this.OnEtatPartieChanging(value);
                    this.SendPropertyChanging();
                    this._EtatPartie = value;
                    this.SendPropertyChanged("EtatPartie");
                    this.OnEtatPartieChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for HeureDebut in the schema.
        /// </summary>
        [Column(Name = @"heureDebut", Storage = "_HeureDebut", CanBeNull = false, DbType = "TIMESTAMP NOT NULL", UpdateCheck = UpdateCheck.Never)]
        public System.DateTime HeureDebut
        {
            get
            {
                return this._HeureDebut;
            }
            set
            {
                if (this._HeureDebut != value)
                {
                    this.OnHeureDebutChanging(value);
                    this.SendPropertyChanging();
                    this._HeureDebut = value;
                    this.SendPropertyChanged("HeureDebut");
                    this.OnHeureDebutChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for HeureFin in the schema.
        /// </summary>
        [Column(Name = @"heureFin", Storage = "_HeureFin", DbType = "TIMESTAMP NULL", UpdateCheck = UpdateCheck.Never)]
        public System.Nullable<System.DateTime> HeureFin
        {
            get
            {
                return this._HeureFin;
            }
            set
            {
                if (this._HeureFin != value)
                {
                    this.OnHeureFinChanging(value);
                    this.SendPropertyChanging();
                    this._HeureFin = value;
                    this.SendPropertyChanged("HeureFin");
                    this.OnHeureFinChanged();
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DbCoups in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbPartie_DbCoup", Storage="_DbCoups", ThisKey="IdPartie", OtherKey="IdPartie", DeleteRule="NO ACTION")]
        public EntitySet<DbCoup> DbCoups
        {
            get
            {
                return this._DbCoups;
            }
            set
            {
                this._DbCoups.Assign(value);
            }
        }

    
        /// <summary>
        /// There are no comments for DbJoueurs_IdJoueurBlanc in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbJoueur_DbPartie", Storage="_DbJoueurs_IdJoueurBlanc", ThisKey="IdJoueurBlanc", OtherKey="IdJoueur", IsForeignKey=true)]
        public DbJoueur DbJoueurs_IdJoueurBlanc
        {
            get
            {
                return this._DbJoueurs_IdJoueurBlanc.Entity;
            }
            set
            {
                DbJoueur previousValue = this._DbJoueurs_IdJoueurBlanc.Entity;
                if ((previousValue != value) || (this._DbJoueurs_IdJoueurBlanc.HasLoadedOrAssignedValue == false))
                {
                    this.SendPropertyChanging();
                    if (previousValue != null)
                    {
                        this._DbJoueurs_IdJoueurBlanc.Entity = null;
                        previousValue.DbParties_IdJoueurBlanc.Remove(this);
                    }
                    this._DbJoueurs_IdJoueurBlanc.Entity = value;
                    if (value != null)
                    {
                        this._IdJoueurBlanc = value.IdJoueur;
                        value.DbParties_IdJoueurBlanc.Add(this);
                    }
                    else
                    {
                        this._IdJoueurBlanc = default(System.Nullable<long>);
                    }
                    this.SendPropertyChanged("DbJoueurs_IdJoueurBlanc");
                }
            }
        }

    
        /// <summary>
        /// There are no comments for DbJoueurs_IdJoueurNoir in the schema.
        /// </summary>
        [Devart.Data.Linq.Mapping.Association(Name="DbJoueur_DbPartie1", Storage="_DbJoueurs_IdJoueurNoir", ThisKey="IdJoueurNoir", OtherKey="IdJoueur", IsForeignKey=true)]
        public DbJoueur DbJoueurs_IdJoueurNoir
        {
            get
            {
                return this._DbJoueurs_IdJoueurNoir.Entity;
            }
            set
            {
                DbJoueur previousValue = this._DbJoueurs_IdJoueurNoir.Entity;
                if ((previousValue != value) || (this._DbJoueurs_IdJoueurNoir.HasLoadedOrAssignedValue == false))
                {
                    this.SendPropertyChanging();
                    if (previousValue != null)
                    {
                        this._DbJoueurs_IdJoueurNoir.Entity = null;
                        previousValue.DbParties_IdJoueurNoir.Remove(this);
                    }
                    this._DbJoueurs_IdJoueurNoir.Entity = value;
                    if (value != null)
                    {
                        this._IdJoueurNoir = value.IdJoueur;
                        value.DbParties_IdJoueurNoir.Add(this);
                    }
                    else
                    {
                        this._IdJoueurNoir = default(long);
                    }
                    this.SendPropertyChanged("DbJoueurs_IdJoueurNoir");
                }
            }
        }
   
        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanging(System.String propertyName) 
        {    
		        var handler = this.PropertyChanging;
            if (handler != null)
                handler(this, new PropertyChangingEventArgs(propertyName));
        }

        protected virtual void SendPropertyChanged(System.String propertyName)
        {    
		        var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void attach_DbCoups(DbCoup entity)
        {
            this.SendPropertyChanging("DbCoups");
            entity.DbPartie = this;
        }
    
        private void detach_DbCoups(DbCoup entity)
        {
            this.SendPropertyChanging("DbCoups");
            entity.DbPartie = null;
        }
    }

}
