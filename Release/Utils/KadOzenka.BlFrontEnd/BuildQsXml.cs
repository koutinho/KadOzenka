using Core.Register.QuerySubsystem;
using Core.Shared.Extensions;
using ObjectModel.Sud;
using System;
using System.Collections.Generic;
using System.Text;

namespace KadOzenka.BlFrontEnd
{
    public static class BuildQsXml
    {
		public static void BuildSudApproveStatus()
		{
			var qsColumn = new QSColumnIf
			{
				Blocks = new List<QSColumnIfBlock>
				{
					new QSColumnIfBlock
					{
						Condition = new QSConditionGroup
						{
							Type = QSConditionGroupType.Or,
							Conditions = new List<QSCondition>
							{
								// Отчеты
								new QSConditionSimple
								{
									ConditionType = QSConditionType.Exists,
									LeftOperand = new QSColumnQuery
									{
										SubQuery = new QSQuery
										{
											MainRegisterID = OMOtchetLinkStatus.GetRegisterId(),
											Columns = new List<QSColumn>
											{
												new QSColumnConstant(1)
											},
											ManualJoin = true,
											Joins = new List<QSJoin>
											{
												new QSJoin
												{
													RegisterId = OMOtchetStatus.GetRegisterId(),
													JoinCondition = new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMOtchetStatus.GetColumn(x => x.Id),
														RightOperand = OMOtchetLinkStatus.GetColumn(x => x.IdOtchet)
													}
												}
											},
											Condition = new QSConditionGroup
											{
												Type = QSConditionGroupType.And,
												Conditions = new List<QSCondition>
												{
													new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMOtchetLinkStatus.GetColumn(x => x.IdObject),
														RightOperand = OMObject.GetColumn(x => x.Id),
														RightOperandLevel = 1
													},
													new QSConditionGroup
													{
														Type = QSConditionGroupType.Or,
														Conditions = new List<QSCondition>
														{
															OMOtchetLinkStatus.GetCondition(x => x.Status == 0),
															OMOtchetStatus.GetCondition(x => x.Status == 0)
														}
													}
												}
											}
										}
									}
								},

								// Заключение
								new QSConditionSimple
								{
									ConditionType = QSConditionType.Exists,
									LeftOperand = new QSColumnQuery
									{
										SubQuery = new QSQuery
										{
											MainRegisterID = OMZakLinkStatus.GetRegisterId(),
											Columns = new List<QSColumn>
											{
												new QSColumnConstant(1)
											},
											ManualJoin = true,
											Joins = new List<QSJoin>
											{
												new QSJoin
												{
													RegisterId = OMZakStatus.GetRegisterId(),
													JoinCondition = new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMZakStatus.GetColumn(x => x.Id),
														RightOperand = OMZakLinkStatus.GetColumn(x => x.IdZak)
													}
												}
											},
											Condition = new QSConditionGroup
											{
												Type = QSConditionGroupType.And,
												Conditions = new List<QSCondition>
												{
													new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMZakLinkStatus.GetColumn(x => x.IdObject),
														RightOperand = OMObject.GetColumn(x => x.Id),
														RightOperandLevel = 1
													},
													new QSConditionGroup
													{
														Type = QSConditionGroupType.Or,
														Conditions = new List<QSCondition>
														{
															OMZakLinkStatus.GetCondition(x => x.Status == 0),
															OMZakStatus.GetCondition(x => x.Status == 0)
														}
													}
												}
											}
										}
									}
								},

								// Судебное решение
								new QSConditionSimple
								{
									ConditionType = QSConditionType.Exists,
									LeftOperand = new QSColumnQuery
									{
										SubQuery = new QSQuery
										{
											MainRegisterID = OMSudLinkStatus.GetRegisterId(),
											Columns = new List<QSColumn>
											{
												new QSColumnConstant(1)
											},
											ManualJoin = true,
											Joins = new List<QSJoin>
											{
												new QSJoin
												{
													RegisterId = OMSudStatus.GetRegisterId(),
													JoinCondition = new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMSudStatus.GetColumn(x => x.Id),
														RightOperand = OMSudLinkStatus.GetColumn(x => x.IdSud)
													}
												}
											},
											Condition = new QSConditionGroup
											{
												Type = QSConditionGroupType.And,
												Conditions = new List<QSCondition>
												{
													new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMSudLinkStatus.GetColumn(x => x.IdObject),
														RightOperand = OMObject.GetColumn(x => x.Id),
														RightOperandLevel = 1
													},
													new QSConditionGroup
													{
														Type = QSConditionGroupType.Or,
														Conditions = new List<QSCondition>
														{
															OMSudLinkStatus.GetCondition(x => x.Status == 0),
															OMSudStatus.GetCondition(x => x.Status == 0)
														}
													}
												}
											}
										}
									}
								},

								// Статус объекта
								new QSConditionSimple
								{
									ConditionType = QSConditionType.Exists,
									LeftOperand = new QSColumnQuery
									{
										SubQuery = new QSQuery
										{
											MainRegisterID = OMObjectStatus.GetRegisterId(),
											Columns = new List<QSColumn>
											{
												new QSColumnConstant(1)
											},
											Condition = new QSConditionGroup
											{
												Type = QSConditionGroupType.And,
												Conditions = new List<QSCondition>
												{
													new QSConditionSimple
													{
														ConditionType = QSConditionType.Equal,
														LeftOperand = OMObjectStatus.GetColumn(x => x.Id),
														RightOperand = OMObject.GetColumn(x => x.Id),
														RightOperandLevel = 1
													},
													OMObjectStatus.GetCondition(x => x.Status == false)
												}
											}
										}
									}
								},
							}
						},
						Result = new QSColumnConstant(0)
					},
					new QSColumnIfBlock
					{
						Result = new QSColumnConstant(1)
					}
				}
			};

			var qsXml = qsColumn.SerializeToXml<QSColumn>();
		}
    }
}
