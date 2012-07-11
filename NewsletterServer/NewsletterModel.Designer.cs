﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("NewsletterModel", "FK_Subscribers_Newsletters", "Newsletters", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(NewsletterServer.Newsletter), "Subscribers", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(NewsletterServer.Subscriber), true)]
[assembly: EdmRelationshipAttribute("NewsletterModel", "FK_Messages_Newsletters", "Newsletter", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(NewsletterServer.Newsletter), "Message", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(NewsletterServer.Message), true)]
[assembly: EdmRelationshipAttribute("NewsletterModel", "FK_Users_Newsletters", "Newsletter", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(NewsletterServer.Newsletter), "Users", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(NewsletterServer.Users), true)]

#endregion

namespace NewsletterServer
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class NewsletterEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new NewsletterEntities object using the connection string found in the 'NewsletterEntities' section of the application configuration file.
        /// </summary>
        public NewsletterEntities() : base("name=NewsletterEntities", "NewsletterEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NewsletterEntities object.
        /// </summary>
        public NewsletterEntities(string connectionString) : base(connectionString, "NewsletterEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new NewsletterEntities object.
        /// </summary>
        public NewsletterEntities(EntityConnection connection) : base(connection, "NewsletterEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Message> Messages
        {
            get
            {
                if ((_Messages == null))
                {
                    _Messages = base.CreateObjectSet<Message>("Messages");
                }
                return _Messages;
            }
        }
        private ObjectSet<Message> _Messages;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Newsletter> Newsletters
        {
            get
            {
                if ((_Newsletters == null))
                {
                    _Newsletters = base.CreateObjectSet<Newsletter>("Newsletters");
                }
                return _Newsletters;
            }
        }
        private ObjectSet<Newsletter> _Newsletters;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Subscriber> Subscribers
        {
            get
            {
                if ((_Subscribers == null))
                {
                    _Subscribers = base.CreateObjectSet<Subscriber>("Subscribers");
                }
                return _Subscribers;
            }
        }
        private ObjectSet<Subscriber> _Subscribers;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Users> Users
        {
            get
            {
                if ((_Users == null))
                {
                    _Users = base.CreateObjectSet<Users>("Users");
                }
                return _Users;
            }
        }
        private ObjectSet<Users> _Users;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Messages EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToMessages(Message message)
        {
            base.AddObject("Messages", message);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Newsletters EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToNewsletters(Newsletter newsletter)
        {
            base.AddObject("Newsletters", newsletter);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Subscribers EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSubscribers(Subscriber subscriber)
        {
            base.AddObject("Subscribers", subscriber);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Users EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToUsers(Users users)
        {
            base.AddObject("Users", users);
        }

        #endregion
        #region Function Imports
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        /// <param name="username">No Metadata Documentation available.</param>
        /// <param name="password">No Metadata Documentation available.</param>
        /// <param name="ret">No Metadata Documentation available.</param>
        public int GetUserNewsletter(global::System.String username, global::System.String password, ObjectParameter ret)
        {
            ObjectParameter usernameParameter;
            if (username != null)
            {
                usernameParameter = new ObjectParameter("username", username);
            }
            else
            {
                usernameParameter = new ObjectParameter("username", typeof(global::System.String));
            }
    
            ObjectParameter passwordParameter;
            if (password != null)
            {
                passwordParameter = new ObjectParameter("password", password);
            }
            else
            {
                passwordParameter = new ObjectParameter("password", typeof(global::System.String));
            }
    
            return base.ExecuteFunction("GetUserNewsletter", usernameParameter, passwordParameter, ret);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NewsletterModel", Name="Message")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Message : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Message object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="date">Initial value of the date property.</param>
        /// <param name="newsletter">Initial value of the newsletter property.</param>
        /// <param name="subject">Initial value of the subject property.</param>
        /// <param name="text">Initial value of the text property.</param>
        /// <param name="clean_text">Initial value of the clean_text property.</param>
        public static Message CreateMessage(global::System.Int32 id, global::System.DateTime date, global::System.Int32 newsletter, global::System.String subject, global::System.String text, global::System.String clean_text)
        {
            Message message = new Message();
            message.id = id;
            message.date = date;
            message.newsletter = newsletter;
            message.subject = subject;
            message.text = text;
            message.clean_text = clean_text;
            return message;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                OndateChanging(value);
                ReportPropertyChanging("date");
                _date = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("date");
                OndateChanged();
            }
        }
        private global::System.DateTime _date;
        partial void OndateChanging(global::System.DateTime value);
        partial void OndateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 newsletter
        {
            get
            {
                return _newsletter;
            }
            set
            {
                OnnewsletterChanging(value);
                ReportPropertyChanging("newsletter");
                _newsletter = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("newsletter");
                OnnewsletterChanged();
            }
        }
        private global::System.Int32 _newsletter;
        partial void OnnewsletterChanging(global::System.Int32 value);
        partial void OnnewsletterChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String subject
        {
            get
            {
                return _subject;
            }
            set
            {
                OnsubjectChanging(value);
                ReportPropertyChanging("subject");
                _subject = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("subject");
                OnsubjectChanged();
            }
        }
        private global::System.String _subject;
        partial void OnsubjectChanging(global::System.String value);
        partial void OnsubjectChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String text
        {
            get
            {
                return _text;
            }
            set
            {
                OntextChanging(value);
                ReportPropertyChanging("text");
                _text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("text");
                OntextChanged();
            }
        }
        private global::System.String _text;
        partial void OntextChanging(global::System.String value);
        partial void OntextChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String clean_text
        {
            get
            {
                return _clean_text;
            }
            set
            {
                Onclean_textChanging(value);
                ReportPropertyChanging("clean_text");
                _clean_text = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("clean_text");
                Onclean_textChanged();
            }
        }
        private global::System.String _clean_text;
        partial void Onclean_textChanging(global::System.String value);
        partial void Onclean_textChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Messages_Newsletters", "Newsletter")]
        public Newsletter Newsletters
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Messages_Newsletters", "Newsletter").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Messages_Newsletters", "Newsletter").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Newsletter> NewslettersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Messages_Newsletters", "Newsletter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Newsletter>("NewsletterModel.FK_Messages_Newsletters", "Newsletter", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NewsletterModel", Name="Newsletter")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Newsletter : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Newsletter object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="name">Initial value of the name property.</param>
        public static Newsletter CreateNewsletter(global::System.Int32 id, global::System.String name)
        {
            Newsletter newsletter = new Newsletter();
            newsletter.id = id;
            newsletter.name = name;
            return newsletter;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String name
        {
            get
            {
                return _name;
            }
            set
            {
                OnnameChanging(value);
                ReportPropertyChanging("name");
                _name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("name");
                OnnameChanged();
            }
        }
        private global::System.String _name;
        partial void OnnameChanging(global::System.String value);
        partial void OnnameChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Subscribers_Newsletters", "Subscribers")]
        public EntityCollection<Subscriber> Subscribers
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Subscriber>("NewsletterModel.FK_Subscribers_Newsletters", "Subscribers");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Subscriber>("NewsletterModel.FK_Subscribers_Newsletters", "Subscribers", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Messages_Newsletters", "Message")]
        public EntityCollection<Message> Messages
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Message>("NewsletterModel.FK_Messages_Newsletters", "Message");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Message>("NewsletterModel.FK_Messages_Newsletters", "Message", value);
                }
            }
        }
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Users_Newsletters", "Users")]
        public EntityCollection<Users> Users
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Users>("NewsletterModel.FK_Users_Newsletters", "Users");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Users>("NewsletterModel.FK_Users_Newsletters", "Users", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NewsletterModel", Name="Subscriber")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Subscriber : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Subscriber object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="name">Initial value of the name property.</param>
        /// <param name="contact">Initial value of the contact property.</param>
        /// <param name="newsletter">Initial value of the newsletter property.</param>
        public static Subscriber CreateSubscriber(global::System.Int32 id, global::System.String name, global::System.String contact, global::System.Int32 newsletter)
        {
            Subscriber subscriber = new Subscriber();
            subscriber.id = id;
            subscriber.name = name;
            subscriber.contact = contact;
            subscriber.newsletter = newsletter;
            return subscriber;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String name
        {
            get
            {
                return _name;
            }
            set
            {
                OnnameChanging(value);
                ReportPropertyChanging("name");
                _name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("name");
                OnnameChanged();
            }
        }
        private global::System.String _name;
        partial void OnnameChanging(global::System.String value);
        partial void OnnameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String contact
        {
            get
            {
                return _contact;
            }
            set
            {
                OncontactChanging(value);
                ReportPropertyChanging("contact");
                _contact = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("contact");
                OncontactChanged();
            }
        }
        private global::System.String _contact;
        partial void OncontactChanging(global::System.String value);
        partial void OncontactChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 newsletter
        {
            get
            {
                return _newsletter;
            }
            set
            {
                OnnewsletterChanging(value);
                ReportPropertyChanging("newsletter");
                _newsletter = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("newsletter");
                OnnewsletterChanged();
            }
        }
        private global::System.Int32 _newsletter;
        partial void OnnewsletterChanging(global::System.Int32 value);
        partial void OnnewsletterChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Subscribers_Newsletters", "Newsletters")]
        public Newsletter Newsletter1
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Subscribers_Newsletters", "Newsletters").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Subscribers_Newsletters", "Newsletters").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Newsletter> Newsletter1Reference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Subscribers_Newsletters", "Newsletters");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Newsletter>("NewsletterModel.FK_Subscribers_Newsletters", "Newsletters", value);
                }
            }
        }

        #endregion
    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="NewsletterModel", Name="Users")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Users : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Users object.
        /// </summary>
        /// <param name="id">Initial value of the id property.</param>
        /// <param name="username">Initial value of the username property.</param>
        /// <param name="password">Initial value of the password property.</param>
        /// <param name="newsletter">Initial value of the newsletter property.</param>
        public static Users CreateUsers(global::System.Int32 id, global::System.String username, global::System.String password, global::System.Int32 newsletter)
        {
            Users users = new Users();
            users.id = id;
            users.username = username;
            users.password = password;
            users.newsletter = newsletter;
            return users;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 id
        {
            get
            {
                return _id;
            }
            set
            {
                if (_id != value)
                {
                    OnidChanging(value);
                    ReportPropertyChanging("id");
                    _id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("id");
                    OnidChanged();
                }
            }
        }
        private global::System.Int32 _id;
        partial void OnidChanging(global::System.Int32 value);
        partial void OnidChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String username
        {
            get
            {
                return _username;
            }
            set
            {
                OnusernameChanging(value);
                ReportPropertyChanging("username");
                _username = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("username");
                OnusernameChanged();
            }
        }
        private global::System.String _username;
        partial void OnusernameChanging(global::System.String value);
        partial void OnusernameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String password
        {
            get
            {
                return _password;
            }
            set
            {
                OnpasswordChanging(value);
                ReportPropertyChanging("password");
                _password = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("password");
                OnpasswordChanged();
            }
        }
        private global::System.String _password;
        partial void OnpasswordChanging(global::System.String value);
        partial void OnpasswordChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 newsletter
        {
            get
            {
                return _newsletter;
            }
            set
            {
                OnnewsletterChanging(value);
                ReportPropertyChanging("newsletter");
                _newsletter = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("newsletter");
                OnnewsletterChanged();
            }
        }
        private global::System.Int32 _newsletter;
        partial void OnnewsletterChanging(global::System.Int32 value);
        partial void OnnewsletterChanged();

        #endregion
    
        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("NewsletterModel", "FK_Users_Newsletters", "Newsletter")]
        public Newsletter Newsletters
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Users_Newsletters", "Newsletter").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Users_Newsletters", "Newsletter").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Newsletter> NewslettersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Newsletter>("NewsletterModel.FK_Users_Newsletters", "Newsletter");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Newsletter>("NewsletterModel.FK_Users_Newsletters", "Newsletter", value);
                }
            }
        }

        #endregion
    }

    #endregion
    
}