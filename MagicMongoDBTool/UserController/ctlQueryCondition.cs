﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MagicMongoDBTool.Module;

namespace MagicMongoDBTool
{
    public partial class ctlQueryCondition : UserControl
    {
        public List<String> ColumnList;

        public ctlQueryCondition()
        {
            InitializeComponent();
        }

        public Boolean IsSeted
        {
            get { return (cmbCompareOpr.SelectedIndex != -1); }
        }

        public DataFilter.QueryConditionInputItem ConditionItem
        {
            get
            {
                if (cmbCompareOpr.SelectedIndex != -1)
                {
                    var rtn = new DataFilter.QueryConditionInputItem();
                    rtn.Compare = (DataFilter.CompareEnum) cmbCompareOpr.SelectedIndex;
                    rtn.Value = new BsonValueEx(ElBsonValue.getValue());
                    rtn.StartMark = cmbStartMark.Text;
                    rtn.EndMark = cmbEndMark.Text;
                    rtn.ColName = cmbColName.Text;
                    return rtn;
                }
                return new DataFilter.QueryConditionInputItem();
            }
            set
            {
                cmbColName.Text = value.ColName;
                ElBsonValue.setValue(value.Value.GetBsonValue());
                cmbCompareOpr.SelectedIndex = (int) value.Compare;
                cmbStartMark.Text = value.StartMark;
                cmbEndMark.Text = value.EndMark;
            }
        }

        public void Init(List<String> ColumnList)
        {
            cmbStartMark.Items.Add(" ");
            cmbStartMark.Items.Add("(");
            cmbStartMark.SelectedIndex = 0;

            cmbEndMark.Items.Add(" ");
            cmbEndMark.Items.Add(MongoDbHelper.EndMark_AND);
            cmbEndMark.Items.Add(MongoDbHelper.EndMark_OR);
            cmbEndMark.Items.Add(MongoDbHelper.EndMark_AND_T);
            cmbEndMark.Items.Add(MongoDbHelper.EndMark_OR_T);
            cmbEndMark.Items.Add(MongoDbHelper.EndMark_T);
            cmbEndMark.SelectedIndex = 0;

            //字段表的载入
            foreach (string item in ColumnList)
            {
                cmbColName.Items.Add(item);
            }
            //逻辑操作符号的载入
            foreach (string item in Enum.GetNames(typeof (DataFilter.CompareEnum)))
            {
                cmbCompareOpr.Items.Add(item);
            }
        }
    }
}