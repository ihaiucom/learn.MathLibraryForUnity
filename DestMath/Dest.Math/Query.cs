using System;

namespace Dest.Math
{
	internal class Query
	{
		protected Query()
		{
		}

		public static bool Sort(ref int v0, ref int v1)
		{
			if (v0 < v1)
			{
				return true;
			}
			int num = v0;
			v0 = v1;
			v1 = num;
			return false;
		}

		public static bool Sort(ref int v0, ref int v1, ref int v2)
		{
			int num;
			int num2;
			int num3;
			bool result;
			if (v0 < v1)
			{
				if (v2 < v0)
				{
					num = v2;
					num2 = v0;
					num3 = v1;
					result = true;
				}
				else if (v2 < v1)
				{
					num = v0;
					num2 = v2;
					num3 = v1;
					result = false;
				}
				else
				{
					num = v0;
					num2 = v1;
					num3 = v2;
					result = true;
				}
			}
			else if (v2 < v1)
			{
				num = v2;
				num2 = v1;
				num3 = v0;
				result = false;
			}
			else if (v2 < v0)
			{
				num = v1;
				num2 = v2;
				num3 = v0;
				result = true;
			}
			else
			{
				num = v1;
				num2 = v0;
				num3 = v2;
				result = false;
			}
			v0 = num;
			v1 = num2;
			v2 = num3;
			return result;
		}

		public static bool Sort(ref int v0, ref int v1, ref int v2, ref int v3)
		{
			int num;
			int num2;
			int num3;
			int num4;
			bool result;
			if (v0 < v1)
			{
				if (v2 < v3)
				{
					if (v1 < v2)
					{
						num = v0;
						num2 = v1;
						num3 = v2;
						num4 = v3;
						result = true;
					}
					else if (v3 < v0)
					{
						num = v2;
						num2 = v3;
						num3 = v0;
						num4 = v1;
						result = true;
					}
					else if (v2 < v0)
					{
						if (v3 < v1)
						{
							num = v2;
							num2 = v0;
							num3 = v3;
							num4 = v1;
							result = false;
						}
						else
						{
							num = v2;
							num2 = v0;
							num3 = v1;
							num4 = v3;
							result = true;
						}
					}
					else if (v3 < v1)
					{
						num = v0;
						num2 = v2;
						num3 = v3;
						num4 = v1;
						result = true;
					}
					else
					{
						num = v0;
						num2 = v2;
						num3 = v1;
						num4 = v3;
						result = false;
					}
				}
				else if (v1 < v3)
				{
					num = v0;
					num2 = v1;
					num3 = v3;
					num4 = v2;
					result = false;
				}
				else if (v2 < v0)
				{
					num = v3;
					num2 = v2;
					num3 = v0;
					num4 = v1;
					result = false;
				}
				else if (v3 < v0)
				{
					if (v2 < v1)
					{
						num = v3;
						num2 = v0;
						num3 = v2;
						num4 = v1;
						result = true;
					}
					else
					{
						num = v3;
						num2 = v0;
						num3 = v1;
						num4 = v2;
						result = false;
					}
				}
				else if (v2 < v1)
				{
					num = v0;
					num2 = v3;
					num3 = v2;
					num4 = v1;
					result = false;
				}
				else
				{
					num = v0;
					num2 = v3;
					num3 = v1;
					num4 = v2;
					result = true;
				}
			}
			else if (v2 < v3)
			{
				if (v0 < v2)
				{
					num = v1;
					num2 = v0;
					num3 = v2;
					num4 = v3;
					result = false;
				}
				else if (v3 < v1)
				{
					num = v2;
					num2 = v3;
					num3 = v1;
					num4 = v0;
					result = false;
				}
				else if (v2 < v1)
				{
					if (v3 < v0)
					{
						num = v2;
						num2 = v1;
						num3 = v3;
						num4 = v0;
						result = true;
					}
					else
					{
						num = v2;
						num2 = v1;
						num3 = v0;
						num4 = v3;
						result = false;
					}
				}
				else if (v3 < v0)
				{
					num = v1;
					num2 = v2;
					num3 = v3;
					num4 = v0;
					result = false;
				}
				else
				{
					num = v1;
					num2 = v2;
					num3 = v0;
					num4 = v3;
					result = true;
				}
			}
			else if (v0 < v3)
			{
				num = v1;
				num2 = v0;
				num3 = v3;
				num4 = v2;
				result = true;
			}
			else if (v2 < v1)
			{
				num = v3;
				num2 = v2;
				num3 = v1;
				num4 = v0;
				result = true;
			}
			else if (v3 < v1)
			{
				if (v2 < v0)
				{
					num = v3;
					num2 = v1;
					num3 = v2;
					num4 = v0;
					result = false;
				}
				else
				{
					num = v3;
					num2 = v1;
					num3 = v0;
					num4 = v2;
					result = true;
				}
			}
			else if (v2 < v0)
			{
				num = v1;
				num2 = v3;
				num3 = v2;
				num4 = v0;
				result = true;
			}
			else
			{
				num = v1;
				num2 = v3;
				num3 = v0;
				num4 = v2;
				result = false;
			}
			v0 = num;
			v1 = num2;
			v2 = num3;
			v3 = num4;
			return result;
		}
	}
}
